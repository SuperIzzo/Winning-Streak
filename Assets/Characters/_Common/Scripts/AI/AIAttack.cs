using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

	public GameObject player;
	public GameObject soundManager;

	GameObject continueText;
	GameObject thisScoreText;
	GameObject highScoreText;
	public GameObject continueBlur;

	//if the player is in this radius then the enemy will start testing if
	//it should attack using AIStats::chaseChance
	public float chaseRadius;
	public float attackRadius;

	//test if the enemy should chase
	public float attackTimer = 0;
	float attackTime;

	bool doChase  = false;
	bool isAttacking = false;
	bool playerDead = false;
	bool hitPlayer = false;


	bool dead = false;
	float deadTimer = 0;

	//following variables
	float angle;
	Vector3 velocity;

	public static bool endGame = false;
	public static bool instance = false;
	public static bool instanceDisable = false;
	float countDownTimer = 11;

	//running speed for running after the player
	public float speed = 0;
	
	//willingness to chase the player 0 - 100 %
	public float chaseChance = 0;
	
	//recovery time to be normal after an attack
	public float recoveryTime = 0;


	public float giveUpChance = 0;
	const float GIVE_UP_MAX = 3;
	float giveUpTimer = 0;


	//chase variables
	Vector3 offset = new Vector3();
	float offsetTimer = 0;


	public Animator animator;
	public GameObject damager;

	private Commentator commentator;

	// Use this for initialization
	void Start () {
		Time.timeScale = 1;
		attackTime = attackTimer;

		player = GameObject.FindGameObjectWithTag("Player");

		continueText = GameObject.FindGameObjectWithTag("continueText");
		thisScoreText = GameObject.FindGameObjectWithTag("thisScore");
		highScoreText = GameObject.FindGameObjectWithTag("highScore");

		continueBlur = GameObject.FindGameObjectWithTag("continueBlur");

		soundManager = GameObject.FindGameObjectWithTag("SoundManager");

		if(!instanceDisable)
		{
			instanceDisable = true;
		}

		Vector3 newPos = this.transform.position;

		if(this.name != "Mascot")
			newPos.y = 0.6499841f;

		this.transform.position = newPos;

		commentator = Commentator.instance;
	}

	void Update () 
	{
//		if(dead)
//		{
//			deadTimer += Time.deltaTime;
//
//			if(deadTimer > 3)
//			{
//				this.rigidbody.Sleep();
//				//dead = false;
//			}
//		}

		// HACK: If we're attacking, don't do ANYTHING
		if( isAttacking )
		{
			return;
		}


		if(playerDead)
		{
			this.rigidbody.Sleep();
			return;
		}

		if(doChase)
		{
			giveUpTimer += Time.deltaTime;

			if(giveUpTimer > GIVE_UP_MAX)
			{
				if(Random.Range(0.0f,100.0f) < giveUpChance + Vector3.Distance(this.transform.position,player.transform.position))
				{
					attackTime = attackTimer;
					isAttacking = false;
					doChase = false;

					return;
				}

				giveUpTimer = 0;
			}

			ChasePlayer ();
		}
		else
		{
			attackTime -= Time.deltaTime;

			if(attackTime < 0)
			{
				if(InRange())
				{
					if(Random.Range(0.0f, 100.0f) < chaseChance)
					{
						doChase = true;
					}
				}

				attackTime = attackTimer;
			}
		}
	}

	void ChasePlayer()
	{
		offsetTimer += Time.deltaTime;

		if(offsetTimer > 4)
		{
			offset = new Vector3(Random.Range(-3.0f,3.0f),
			                     0,
			                     Random.Range(-3.0f,3.0f));

			if(Random.Range (0.0f,100.0f) < 10)
			{
				offset += new Vector3(Vector3.Distance(this.transform.position,player.transform.position),
				                      Vector3.Distance(this.transform.position,player.transform.position),
				                      Vector3.Distance(this.transform.position,player.transform.position));
			}

			offsetTimer = 0;
		}

//		if(Random.value < 0.001f)
//		{
//			Debug.Log ("Repath");
//			offsetTimer = 5;
//		}

		angle = Mathf.Atan2(this.transform.position.x - (player.transform.position.x),// + offset.x),
		                    this.transform.position.z - (player.transform.position.z));// + offset.z));

		velocity = -new Vector3(Mathf.Sin (angle),
		                               0,
		                               Mathf.Cos (angle));

		this.transform.position += velocity * speed * Time.deltaTime;


		if(InAttackRange())
		{
			AttackPlayer ();

			doChase = false;
			isAttacking = true;

			attackTime = attackTimer + recoveryTime;
		}

		RotateTowardsPlayer(velocity);
	}

	void RotateTowardsPlayer(Vector3 vel)
	{
		//rotate to player
		Vector3 lookAt = player.transform.position;
		this.transform.LookAt(lookAt);

		//stop tilting
		Quaternion rot = this.transform.rotation;
		rot.eulerAngles = new Vector3(0,rot.eulerAngles.y, 0);

		this.transform.rotation = rot;
	}

	//will execute when the enemy is close enough to the player
	//end of game logic here
	void AttackPlayer()
	{
		//dive animation here
		animator.SetBool ( "tackle", true );
		StartCoroutine("DiveForward");

		damager.SetActive(true);
		isAttacking = true;
	}

	public void KillThePlayer()
	{
		if(!endGame)
		{
			Commentator commentator = Commentator.instance;

			if( commentator )
				commentator.Comment( CommentatorEvent.GAME_OVER );

			endGame = true;
			hitPlayer = true;
			soundManager.GetComponent<AudioMan>().PlayEffect("TACKLE1",1);
			soundManager.GetComponent<AudioMan>().PlayTackled();

			BaseCharacterController character = player.GetComponentInChildren<BaseCharacterController>();
			if( character )
				character.KnockDown();
		}
	}

	IEnumerator DiveForward()
	{

		Vector3 vel = this.transform.forward;
		float timer = 0;

		while(timer < 0.3f)
		{
			this.transform.position += vel * Time.deltaTime * 6;
			timer += Time.deltaTime;
			yield return null;
		}

		dead = true;
		this.GetComponent<BoxCollider>().enabled = false;
		this.GetComponent<CapsuleCollider>().enabled = false;
		MissedTackle();
	}

	public void MissedTackle()
	{
		//soundManager.GetComponent<DialogueManager>().PlaySpeech("PLAYER_DODGE");
		if(!hitPlayer && !endGame)
		{
			// Points
			ScoreManager.AddScore(25);
			ScoreManager.AddMultPoint(0.5f);

			soundManager.GetComponent<AudioMan>().PlayTackleDodge();
			soundManager.GetComponent<AudioMan>().BuildHype(0.01f,10,0.1f);

			if( commentator && 0.5f>Random.value)
			{
				commentator.Comment( CommentatorEvent.DODGE_TACKLE );
			}
		}
	}

	public void ForceChase()
	{
		StartCoroutine("ForcePlayerChase");
	}

	IEnumerator ForcePlayerChase()
	{
		float timer = 0;

		while(timer < 10)
		{
			doChase = true;

			yield return null;
		}
	}
	

	//calculate if the player is in range of the enemy
	bool InRange()
	{
		if(Vector3.Distance(this.transform.position,player.transform.position) < chaseRadius)
		{
			return true;
		}
		else
			return false;
	}

	bool InAttackRange()
	{
		if(Vector3.Distance(this.transform.position,player.transform.position) < attackRadius)
		{
			return true;
		}
		else
			return false;
	}
}

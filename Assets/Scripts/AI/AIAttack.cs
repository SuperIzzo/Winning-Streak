using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

	GameObject player;

	//if the player is in this radius then the enemy will start testing if
	//it should attack using AIStats::chaseChance
	public float chaseRadius;
	public float attackRadius;

	//test if the enemy should chase
	public float attackTimer = 0;
	float attackTime;

	bool doChase  = false;
	bool isAttacking = false;

	//following variables
	float angle;
	Vector3 velocity;


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


	// Use this for initialization
	void Start () {
		attackTime = attackTimer;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () 
	{
		if(doChase)
		{
			giveUpTimer += Time.deltaTime;

			if(giveUpTimer > GIVE_UP_MAX)
			{
				if(Random.Range(0.0f,100.0f) < giveUpChance)
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


			offsetTimer = 0;
		}

		angle = Mathf.Atan2(this.transform.position.x - (player.transform.position.x + offset.x),
		                          this.transform.position.z - (player.transform.position.z + offset.z));

		velocity = -new Vector3(Mathf.Sin (angle),
		                               0,
		                               Mathf.Cos (angle));

		this.transform.position += velocity * speed * Time.deltaTime;

		if(InAttackRange())
		{
			AttackPlayer ();

			doChase = false;
			isAttacking = false;

			attackTime = attackTimer + recoveryTime;
		}
	}

	//will execute when the enemy is close enough to the player
	void AttackPlayer()
	{
		//dive animation here
		if(true) //change to collision hit
		{
			player.GetComponentInChildren<GoRagdoll>().KillPlayer();
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

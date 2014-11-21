using UnityEngine;
using System.Collections;

public class AIAttack : MonoBehaviour {

	GameObject player;

	//if the player is in this radius then the enemy will start testing if
	//it should attack using AIStats::chaseChance
	public float attackRadius;

	//test if the enemy should chase
	public float attackTimer = 0;
	float attackTime;

	bool doChase  = true;

	//following variables
	float angle;
	Vector3 velocity;

	// Use this for initialization
	void Start () {
		attackTime = attackTimer;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update () 
	{
		if(doChase)
		{
			ChasePlayer ();
		}
		else
		{
			attackTime -= Time.deltaTime;

			if(attackTime < 0)
			{
				if(InRange())
				{
					if(Random.Range(0.0f, 100.0f) < this.GetComponent<AIStats>().chaseChance)
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
		angle = Mathf.Atan2(this.transform.position.x - player.transform.position.x,
		                          this.transform.position.z - player.transform.position.z);

		velocity = -new Vector3(Mathf.Sin (angle),
		                               0,
		                               Mathf.Cos (angle));

		this.transform.position += velocity * this.GetComponent<AIStats>().speed * Time.deltaTime;
	}

	//calculate if the player is in range of the enemy
	bool InRange()
	{
		if(Vector3.Distance(this.transform.position,player.transform.position) < attackRadius)
			return true;
		else
			return false;
	}
}

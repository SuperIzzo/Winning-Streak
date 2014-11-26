using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//using XInputDotNetPure;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	
	public List<GameObject> spawnPoints = new List<GameObject>();

	public bool physicsMovement = false;
	public float goofiness = 1.0f;

	bool slowmo = false;

	public bool dancing = false;

	public Animator animator;
	public bool canMove = true;

	public float dashInterval = 1;
	float dashTimer = 0;
	float danceTimer = 0;
	bool announcedDancing = false;

	void Start () 
	{
		dashTimer = dashInterval;

		if( animator )
		{
			animator.SetFloat( "goofiness", goofiness );
		}

		if(physicsMovement)
			speed *= 100;
	}

	void Update () 
	{
		if(Input.GetButtonDown ("slowmo"))
		{
			slowmo = !slowmo;
		}
		
		if(slowmo) Time.timeScale = 0.2f; else Time.timeScale = 1;

		if(!canMove)
			return;

		dashTimer -= Time.deltaTime;

		//controller movement
		ProcessInput();
	}


	void ProcessInput()
	{
		dancing = Input.GetButton( "Wiggle" );
		animator.SetBool( "wiggle", dancing );

		if( dancing )
		{
			danceTimer += Time.deltaTime;

			// 1 point per second!
			ScoreManager.AddMultPoint( Time.deltaTime );

			if( danceTimer > 1.0f && !announcedDancing )
			{
				announcedDancing = true;
				// DO the WIGGLE
				//   Game logic to add points etc
				Commentator commentator = Commentator.instance;

				if( commentator )
				{
					commentator.Comment( CommentatorEvent.WIGGLE );
				}
				//comme
			}

			// Process no further input
			// If they player is wiggling he cannot move or 
			// do anything else
			return;
		}
		else
		{
			announcedDancing = false;
			danceTimer = 0;
		}

		
		float x = Input.GetAxis( "Horizontal" );	
		float y = Input.GetAxis( "Vertical" );
		
		Vector2 controlVector = new Vector2(x,y);


		if( controlVector.magnitude> 0 )
		{
			Vector3 moveVec = new Vector3( controlVector.x, 0, controlVector.y );
			moveVec *= speed * Time.deltaTime;

			//player movement
			if(physicsMovement)
			{
				this.rigidbody.AddForce( moveVec );	
			}
			else
			{
				if( animator )
				{
					animator.SetFloat( "speed", controlVector.magnitude );
				}
				transform.position = transform.position + moveVec;
			}

			//player rotation towards direction
			RotatePlayer( moveVec );
		}

		if(dashTimer < 0)
		{
			if( Input.GetButtonDown("Dash") )
			{
				Dash();
				dashTimer = dashInterval;
			}
		} 
	}

	void RotatePlayer( Vector3 moveVector )
	{
		Quaternion targetRot = Quaternion.LookRotation( moveVector, Vector3.up );
		Quaternion rot = this.transform.rotation;

		//smooth transitioning for rotation, also makes the rotation and movement more human like
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation,
		                                           targetRot,
		                                           Time.deltaTime * 6);
	}

	void Dash()
	{
		StartCoroutine("StartDash");
	}

	IEnumerator StartDash()
	{
		Vector3 toPos = this.transform.position + this.transform.forward * 2;
		float timer = 0;

		while(timer < 1)
		{
			timer += Time.deltaTime * 3;

			this.transform.position = Vector3.Lerp(this.transform.position,toPos, Time.deltaTime * 3);

			yield return null;
		}
	}
}





















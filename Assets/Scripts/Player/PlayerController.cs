using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using XInputDotNetPure;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;

	public List<GameObject> spawnPoints = new List<GameObject>();

	public bool physicsMovement = false;

	//XInput stuff
	PlayerIndex index = new PlayerIndex();

	GamePadState controllerState = new GamePadState();
	GamePadState controllerStatePrev = new GamePadState();

	public bool isConnected = false;

	public bool canMove = true;

	public float dashInterval = 1;
	float dashTimer = 0;

	void Start () 
	{
		InitialiseController();
		dashTimer = dashInterval;

		if(physicsMovement)
			speed *= 100;
	}

	void Update () 
	{
		if(!canMove)
			return;

		dashTimer -= Time.deltaTime;

		//controller movement
		//if(isConnected)
		//{
			ControllerMovement();
		//}
		//else
		//{
			KeyboardMovement();
		//}
	}

	void RotatePlayerController()
	{
		Quaternion rot = this.transform.rotation;
		
		rot.eulerAngles = new Vector3(this.transform.eulerAngles.x,
		                              Mathf.Atan2(TestLeftStick("X"),TestLeftStick("Y")) * Mathf.Rad2Deg,
		                              this.transform.eulerAngles.z);
		
		//smooth transitioning for rotation, also makes the rotation and movement more human like
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation,
		                                           rot,
		                                           Time.deltaTime * 6);
	}

	void RotatePlayerKeyboard()
	{
		//todo sort out rotate for keyboard
		Quaternion rot = this.transform.rotation;

		//hardcoded rotation for the keyboard input
		if(Input.GetKey(KeyCode.W)) rot.eulerAngles = new Vector3(0,0,0);
		if(Input.GetKey(KeyCode.D)) rot.eulerAngles = new Vector3(0, 90,0);
		if(Input.GetKey(KeyCode.S)) rot.eulerAngles = new Vector3(0, 180,0);
		if(Input.GetKey(KeyCode.A)) rot.eulerAngles = new Vector3(0,270,0);

		if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S)) rot.eulerAngles = new Vector3(0, 135,0);
		if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S)) rot.eulerAngles = new Vector3(0, 225,0);
		if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W)) rot.eulerAngles = new Vector3(0,315);
		if(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) rot.eulerAngles = new Vector3(0, 45,0);

		//smooth transitioning for rotation, also makes the rotation and movement more human like
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation,
		                                           rot,
		                                           Time.deltaTime * 6);
	}

	void ControllerMovement()
	{
		//player movement
		if(physicsMovement)
		{
			//left/down movement
			if(TestLeftStick("X") != 0)
			{
				this.rigidbody.AddForce(TestLeftStick("X") * speed * Time.deltaTime,0,0);
			}
			
			//up/down movement
			if(TestLeftStick("Y") != 0)
			{
				this.rigidbody.AddForce(0,0,TestLeftStick("Y") * speed * Time.deltaTime);
			}
		}
		else
		{
			//left/down movement
			if(TestLeftStick("X") != 0)
			{
				this.transform.position += new Vector3(TestLeftStick("X") * speed * Time.deltaTime,0,0);
			}
			
			//up/down movement
			if(TestLeftStick("Y") != 0)
			{
				this.transform.position += new Vector3 (0,0,TestLeftStick("Y") * speed * Time.deltaTime);
			}
		}
		
		//player rotation towards direction
		if(TestLeftStick("X") != 0 || TestLeftStick("Y") != 0)
		{
			RotatePlayerController();
		}

		if(dashTimer < 0)
		{
			if(TestButton("X"))
			{
				Debug.Log("Dashed");
				Dash();
				dashTimer = dashInterval;
			}
		}

		UpdateController();
	}

	void KeyboardMovement()
	{
		//player movement
		if(physicsMovement)
		{
			//left movement
			if(Input.GetKey(KeyCode.A))
			{
				this.rigidbody.AddForce(-speed * Time.deltaTime,0,0);
			}

			//right movement
			if(Input.GetKey(KeyCode.D))
			{
				this.rigidbody.AddForce(speed * Time.deltaTime,0,0);
			}

			
			//up movement
			if(Input.GetKey(KeyCode.W))
			{
				this.rigidbody.AddForce(0,0,speed * Time.deltaTime);
			}

			//down
			if(Input.GetKey(KeyCode.S))
			{
				this.rigidbody.AddForce(0,0,-speed * Time.deltaTime);
			}

			RotatePlayerKeyboard();
		}
		else
		{
			//left movement
			if(Input.GetKey(KeyCode.A))
			{
				this.transform.position += new Vector3(-speed * Time.deltaTime,0,0);
			}
			
			//right movement
			if(Input.GetKey(KeyCode.D))
			{
				this.transform.position += new Vector3(speed * Time.deltaTime,0,0);
			}
			
			
			//up movement
			if(Input.GetKey(KeyCode.W))
			{
				this.transform.position += new Vector3(0,0,speed * Time.deltaTime);
			}
			
			//down
			if(Input.GetKey(KeyCode.S))
			{
				this.transform.position += new Vector3(0,0,-speed * Time.deltaTime);
			}

			RotatePlayerKeyboard();
		}

		if(dashTimer < 0)
		{
			if(Input.GetKeyDown(KeyCode.K))
			{
				Dash();
				dashTimer = dashInterval;
			}
		}
	}

	void Dash()
	{
		if(physicsMovement)
		{
			this.rigidbody.AddForce(this.transform.forward * 30000);
		}
		else
		{
			StartCoroutine("StartDash");
			//this.rigidbody.isKinematic = false;
			//this.rigidbody.AddForce(this.transform.forward * 30000);
			//this.transform.position += this.transform.right;
			//this.rigidbody.isKinematic = true;
		}
	}

	IEnumerator StartDash()
	{
		Debug.Log("dash ienum");

		Vector3 toPos = this.transform.position + Vector3.forward;
		float timer = 0;

		while(timer < 1)
		{
			timer += Time.deltaTime * 6;

			this.transform.position = Vector3.Lerp(this.transform.position,toPos, Time.deltaTime * 6);

			yield return null;
		}

		Debug.Log("dashed");
	}


	//CONTROLLER STUFF

	void UpdateController()
	{
		controllerStatePrev = controllerState;
		controllerState = GamePad.GetState(index);
	}
	
	void InitialiseController()
	{
		PlayerIndex testPlayerIndex = (PlayerIndex)0;
		GamePadState testState = GamePad.GetState(testPlayerIndex);

		if(testState.IsConnected)
		{
			Debug.Log ("GamePad found");
			index = testPlayerIndex;
			isConnected = true;
		}
	}

	//controller getters
	public bool TestButton(string button)
	{
		switch(button)
		{
		case "Y":
			if(controllerStatePrev.Buttons.Y == ButtonState.Released &&
			   controllerState.Buttons.Y == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "B":
			if(controllerStatePrev.Buttons.B == ButtonState.Released &&
			   controllerState.Buttons.B == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "A":
			if(controllerStatePrev.Buttons.A == ButtonState.Released &&
			   controllerState.Buttons.A == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "X":
			if(controllerStatePrev.Buttons.X == ButtonState.Released &&
			   controllerState.Buttons.X == ButtonState.Pressed)
			{
				return true;
			}
			break;

		case "LB":
			if(controllerStatePrev.Buttons.LeftShoulder == ButtonState.Released &&
			   controllerState.Buttons.LeftShoulder == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "RB":
			if(controllerStatePrev.Buttons.RightShoulder == ButtonState.Released &&
			   controllerState.Buttons.RightShoulder == ButtonState.Pressed)
			{
				return true;
			}
			break;

		case "START":
			if(controllerStatePrev.Buttons.Start == ButtonState.Released &&
			   controllerState.Buttons.Start == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "BACK":
			if(controllerStatePrev.Buttons.Back == ButtonState.Released &&
			   controllerState.Buttons.Back == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "DPAD-RIGHT":
			if(controllerStatePrev.DPad.Right == ButtonState.Released &&
			   controllerState.DPad.Right == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "DPAD-LEFT":
			if(controllerStatePrev.DPad.Left == ButtonState.Released &&
			   controllerState.DPad.Left == ButtonState.Pressed)
			{
				return true;
			}
			break;
		}

		return false;
	}

	float TestLeftStick(string axis)
	{
		if(axis == "X")
			return controllerState.ThumbSticks.Left.X;
		else if(axis == "Y")
			return controllerState.ThumbSticks.Left.Y;

		return 0;
	}	
	float TestRightStick(string axis)
	{
		if(axis == "X")
			return controllerState.ThumbSticks.Right.X;
		else if(axis == "Y")
			return controllerState.ThumbSticks.Right.Y;
		
		return 0;
	}


	public bool TestButtonDown(string button)
	{
		switch(button)
		{
		case "Y":
			if(controllerState.Buttons.Y == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "B":
			if(controllerState.Buttons.B == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "A":
			if(controllerState.Buttons.A == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "X":
			if(controllerState.Buttons.X == ButtonState.Pressed)
			{
				return true;
			}
			break;
			
		case "LB":
			if(controllerState.Buttons.LeftShoulder == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "RB":
			if(controllerState.Buttons.RightShoulder == ButtonState.Pressed)
			{
				return true;
			}
			break;
			
		case "START":
			if(controllerState.Buttons.Start == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "BACK":
			if(controllerState.Buttons.Back == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "DPAD-RIGHT":
			if(controllerState.DPad.Right == ButtonState.Pressed)
			{
				return true;
			}
			break;
		case "DPAD-LEFT":
			if(controllerState.DPad.Left == ButtonState.Pressed)
			{
				return true;
			}
			break;
		}
		
		return false;
	}
}





















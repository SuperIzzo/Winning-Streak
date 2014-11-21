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

	bool isConnected = false;


	void Start () 
	{
		InitialiseController();
	}

	void Update () 
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
//		if(physicsMovement)
//		{
			if(TestLeftStick("X") != 0 || TestLeftStick("Y") != 0)
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
//		}
//		else
//		{
//
//		}

		UpdateController();

	}

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
	bool TestButton(string button)
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

}





















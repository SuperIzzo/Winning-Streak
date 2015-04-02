using UnityEngine;
using System.Collections;


//--------------------------------------------------------------
/// <summary> An static utility class that provides easy access 
/// to player related objects and components. </summary>
//  Note: In multiplayer this will be indexable 
//--------------------------------------
public class Player
{
	//--------------------------------------------------------------
	/// <summary> Returns player one. </summary>
	//--------------------------------------
	public static Player p1
	{
		get
		{
			if( _p1 == null ) 
				_p1 = new Player();

			return _p1;
		}
	}
	private static Player _p1;


	//--------------------------------------------------------------
	/// <summary> Returns the player gameObject (readonly). </summary>
	/// <value> The player game object.</value>
	//--------------------------------------
	public GameObject gameObject
	{
		get
		{
			if( !_gameObject )
				_gameObject = GameObject.FindGameObjectWithTag( Tags.player );

			return _gameObject;
		}
	}
	private GameObject _gameObject;

	//--------------------------------------------------------------
	/// <summary> Returns the player transform (readonly). </summary>
	/// <value> The player transform.</value>
	//--------------------------------------
	public Transform transform
	{
		get
		{
			if( gameObject )
				return gameObject.transform;
			else
				return null;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Returns the player character controller (readonly). </summary>
	/// <value> The player character controller.</value>
	//--------------------------------------
	public BaseCharacterController characterController
	{
		get
		{
			if( gameObject )
				return gameObject.GetComponent<BaseCharacterController>();
			else
				return null;
		}
	}
}

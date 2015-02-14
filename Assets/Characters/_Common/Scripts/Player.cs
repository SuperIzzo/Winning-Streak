using UnityEngine;
using System.Collections;


//--------------------------------------------------------------
/// <summary> An static utility class that provides easy access 
/// to player related objects and components. </summary>
//  Note: In multiplayer this will be indexable 
//--------------------------------------
public class Player
{
	private static GameObject _gameObject;

	//--------------------------------------------------------------
	/// <summary> Returns the player gameObject (readonly). </summary>
	/// <value> The player game object.</value>
	//--------------------------------------
	public static GameObject gameObject
	{
		get
		{
			if( !_gameObject )
				_gameObject = GameObject.FindGameObjectWithTag( Tags.player );

			return _gameObject;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Returns the player transform (readonly). </summary>
	/// <value> The player transform.</value>
	//--------------------------------------
	public static Transform transform
	{
		get
		{
			if( Player.gameObject )
				return Player.gameObject.transform;
			else
				return null;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Returns the player character controller (readonly). </summary>
	/// <value> The player character controller.</value>
	//--------------------------------------
	public static BaseCharacterController characterController
	{
		get
		{
			if( Player.gameObject )
				return Player.gameObject.GetComponent<BaseCharacterController>();
			else
				return null;
		}
	}
}

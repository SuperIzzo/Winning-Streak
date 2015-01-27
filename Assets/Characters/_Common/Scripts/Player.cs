using UnityEngine;
using System.Collections;


//--------------------------------------------------------------
/// <summary> An static utility class that provides easy access 
/// to player related objects and components. </summary>
//--------------------------------------
public class Player
{
	private static GameObject _gameObject;

	/// <summary> Gets the player gameObject. </summary>
	/// <value>The game object.</value>
	public static GameObject gameObject
	{
		get
		{
			if( !_gameObject )
				_gameObject = GameObject.FindGameObjectWithTag( Tags.player );

			return _gameObject;
		}
	}

	/// <summary> Gets the player transform. </summary>
	/// <value>The transform.</value>
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

	/// <summary> Gets the player character controller. </summary>
	/// <value>The character controller.</value>
	public static BaseCharacterController characterController
	{
		get
		{
			if( Player.gameObject )
			{
				return Player.gameObject.GetComponent<BaseCharacterController>();
			}
			else
			{
				return null;
			}
		}
	}
}

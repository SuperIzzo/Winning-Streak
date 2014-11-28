using UnityEngine;
using System.Collections;

[AddComponentMenu("Character/Player Input")]
[RequireComponent( typeof(BaseCharacterController) )]
public class PlayerInput : MonoBehaviour
{
	private BaseCharacterController controller;
	private static readonly float AXES_DEADZONE = 0.1f;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( !controller )
		{
			// Skip the update and log an error
			Debug.LogError( "No character controller found!" );
			return;
		}

		// Get Input
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector2 controlVector = new Vector2(x,y);

		bool dancing		= Input.GetButton( "Wiggle" );
		bool dashDown		= Input.GetButtonDown( "Dash" );


		// By default axes are separate and map to a unit square
		// However we want the speed along the diagonals to be the same
		// as the speed along the main axes, so we remap the input to
		// a unit sphere (by normalizing and scaling accordingly) 
		if( controlVector.magnitude > AXES_DEADZONE )
		{
			Vector2 unitCircleRemap = controlVector.normalized;
			controlVector.x *= Mathf.Abs( unitCircleRemap.x );
			controlVector.y *= Mathf.Abs( unitCircleRemap.y );
		}
		else
		{
			// Deadzone
			controlVector = Vector2.zero;
		}

		controller.Move( controlVector );
		controller.Dance( dancing );

		if( dashDown )
			controller.Dash();

	}
}

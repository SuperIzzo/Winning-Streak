using UnityEngine;
using System.Collections;

public class AnimTest : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator>();
		//animator.SetFloat( "speed", .1 );
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( animator == null )
		{
			Debug.Log("no animator");
			return;
		}

		if( Input.GetKeyDown( KeyCode.C ) )
		{
			Debug.Log("wiggle = true");
			animator.SetBool( "wiggle", true );
			//animator.speed = .1f;
		}

		if( Input.GetKeyUp( KeyCode.C ) )
		{
			Debug.Log("wiggle = false");
			animator.SetBool( "wiggle", false );
		}
	
		if( Input.GetKeyDown( KeyCode.V ) )
		{
			Debug.Log("throw = true");
			animator.SetBool( "charge_throw", true );
		}
		
		if( Input.GetKeyUp( KeyCode.V ) )
		{
			Debug.Log("throw = false");
			animator.SetBool( "charge_throw", false );
		}

	}
}

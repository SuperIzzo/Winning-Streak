using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

	Animator animator;
	float speed = 0;

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

	}

	public void UpdateSpeed(float spd)
	{
		speed = spd;
	}

	public void SetAnimationOn(string ani)
	{
		switch(ani)
		{
		case "run":
			animator.SetBool( "run", true );
			break;
		case "run_goofy":
			animator.SetBool( "run_goofy", true );
			break;
		case "wiggle":
			animator.SetBool( "wiggle", true );
			break;
		case "charge_throw":
			animator.SetBool( "charge_throw", true );
			break;
		case "tackle":
			animator.SetBool( "tackle", true );
			break;
		}
	}

	public void SetAnimationOff(string ani)
	{
		switch(ani)
		{
		case "run":
			animator.SetBool( "run", false );
			break;
		case "run_goofy":
			animator.SetBool( "run_goofy", false );
			break;
		case "wiggle":
			animator.SetBool( "wiggle", false );
			break;
		case "charge_throw":
			animator.SetBool( "charge_throw", false );
			break;
		case "tackle":
			animator.SetBool( "tackle", false );
			break;
		}
	}
}

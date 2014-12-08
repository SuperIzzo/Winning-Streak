using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseCharacterController))]
public class AIController : MonoBehaviour 
{
	private enum AIStates
	{
		CHASING,		// to tackle
		FOLLOWING,		// an ally
		STANDING,
		ROAMING,
	}

	public float alertRadius = 5.0f;
	public float chaseOffDistance = 10.0f; 

	// Internal links
	BaseCharacterController controller;
	Faction faction;

	// State
	AIStates state;
	Transform target;


	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<BaseCharacterController>();
		faction = GetComponent<Faction>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		switch( state )
		{
		case AIStates.STANDING:
			StandingState();
			break;
		case AIStates.ROAMING:
			RoamingState();
			break;
		case AIStates.CHASING:
			ChasingState();
			break;
		case AIStates.FOLLOWING:
			FollowingState();
			break;
		}
	}


	void StandingState()
	{
		// TODO
		// if player or opponent around - chase
		// if ally around, follow

		if( Random.value > 0.01 )		// Don't do this every frame... just once in a while
		{
			BaseCharacterController enemy = DetectEnemy();
			if( enemy )
			{
				target = enemy.transform;
				state = AIStates.CHASING;
			}
		}
	}

	void RoamingState()
	{
		// if player or opponent around - chase
		// if ally around, follow
		
	}

	void ChasingState()
	{
		// chase until too far away
		// if close enough clinch and tackle
		if( target!=null )
		{
			Vector3 direction = target.position = transform.position;
			float distance = direction.magnitude;
			direction.Normalize();

			controller.Move( direction );

			if( distance > chaseOffDistance )
				state = AIStates.STANDING;
		}
	}

	void FollowingState()
	{
	}


	BaseCharacterController DetectEnemy()
	{
		Collider[] colliders = Physics.OverlapSphere( transform.position, alertRadius );

		foreach( Collider collider in colliders )
		{
			// If it is a different object
			if( collider.transform != transform )
			{
				Faction otherFaction = collider.GetComponent<Faction>();
				BaseCharacterController otherController = collider.GetComponent<BaseCharacterController>();

				if( faction && faction.IsAlly(otherFaction) )
				   continue; // Skip this iteration, we ignore allies

				if( otherController )
				{
					return otherController;
				}
			}
		}

		return null;
	}
}

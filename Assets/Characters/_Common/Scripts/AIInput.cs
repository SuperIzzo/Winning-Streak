using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--------------------------------------------------------------
/// <summary> AI character controller. </summary>
/// <description> AIController is a decision taking component that
/// manipulates a <see cref="BaseCharacterController"/>. It worrks analogous
/// to an input device (such as <see cref="PlayerInput"/>) with the only
/// difference being that it generates the input signals based on some logic.
/// Note: AICharacterController does not and cannot modify the game state,
/// that task is reserved for the BaseCharacterController component.
/// </description>
//--------------------------------------
[AddComponentMenu("Character/AI Input",100)]
[RequireComponent(typeof(BaseCharacterController))]
public class AIInput : MonoBehaviour 
{
	//--------------------------------------------------------------
	#region Public types
	//--------------------------------------

	//--------------------------------------------------------------
	///<summary> Valid states for the AI </summary>
	//--------------------------------------
	public enum AIStates
	{
		/// <summary> When the AI's in persiut
		/// of an enemytarget. </summary>
		CHASING,

		/// <summary> When the AI's following
		/// an ally. </summary>
		FOLLOWING,

		/// <summary> When the AI is cluelesly
		/// roaming around. </summary>
		UNAWARE,
	}
	#endregion


	//--------------------------------------------------------------
	#region Public settings
	//--------------------------------------
	/// <summary> The radius in which the AI will become alerted
	/// if an enemy is close. </summary>
	public float alertRadius = 5.0f;

	/// <summary> The maximal distance between a chasing AI and
	/// it's target, before the AI gives up.</summary>
	public float chaseOffDistance = 10.0f; 

	/// <summary> The rate at which an unaware AI will be changing
	/// its direction </summary>
	public float redirectionRate = 0.01f;

	/// <summary> The rate at which a chasing AI will randomly
	/// decide to give up a chase. </summary>
	public float chaseGiveUpRate = 0.0001f;

	/// <summary> The distance at which a chasing AI will attempt
	/// to a tackle. </summary>
	public float tackleRange	= 2.0f;
	
	/// <summary> The rate at which the player will be targeted
	/// (unfairly) by the AI. </summary>
	public float playerHate		= 0.0f;
	#endregion
	

	//--------------------------------------------------------------
	#region Internal references
	//--------------------------------------
	BaseCharacterController controller;
	Faction faction;
	#endregion


	//--------------------------------------------------------------
	#region State
	//--------------------------------------
	/// <summary> The current AI state. </summary>
	public AIStates					state {get; private set;}

	/// <summary> Chase target.
	/// Only valid in the CHASING AIState. </summary>
	public BaseCharacterController 	target{get; private set;}

	/// <summary> The roaming direction.
	/// Only valid in the UNAWARE AIState. </summary>
	public Vector2					roamingDirection{get; private set;}
	#endregion
	
	//--------------------------------------------------------------
	/// <summary> Use this for initialization. </summary>
	//--------------------------------------
	void Start () 
	{
		controller = GetComponent<BaseCharacterController>();
		faction = GetComponent<Faction>();
		state = AIStates.UNAWARE;
	}

	//--------------------------------------------------------------
	/// <summary> Update is called once per frame. </summary>
	//--------------------------------------
	void Update () 
	{
		switch( state )
		{
		case AIStates.UNAWARE:
			UnawareState();
			break;
		case AIStates.CHASING:
			ChasingState();
			break;
		case AIStates.FOLLOWING:
			FollowingState();
			break;
		}

        KeepFormation();
	}

	//--------------------------------------------------------------
	/// <summary> The state of unawareness </summary>
	//--------------------------------------
	void UnawareState()
	{
		// TODO
		// if player or opponent around - chase
		// if ally around, follow

		// Occasionally start roaming about
		if( Random.value < redirectionRate )
		{		
			roamingDirection = new Vector2( Random.value-0.5f, Random.value-0.5f );
			roamingDirection.Normalize();
		}

		if( roamingDirection.magnitude > 0 )
			controller.Move( roamingDirection * 0.5f );

        if( Random.value < 0.05f )
        {
            BaseCharacterController enemy = DetectEnemy();
            if( enemy )
            {
				target = enemy;
				state = AIStates.CHASING;
            }
        }
	}

	//--------------------------------------------------------------
	/// <summary> State of chasing an enemy. </summary>
	//--------------------------------------
	void ChasingState()
	{
		// chase until too far away
		// if close enough clinch and tackle
		if( target!=null )
		{
			Vector3 direction = target.transform.position - transform.position;
			Vector2 direction2D = new Vector2( direction.x, direction.z ); 
			float distance = direction2D.magnitude;
			direction2D.Normalize();
			controller.Move( direction2D );

			if( distance>chaseOffDistance || chaseGiveUpRate>Random.value || target.isKnockedDown )
			{
				state = AIStates.UNAWARE;
			}

			if( distance < tackleRange )
			{
				controller.Tackle(true);
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Follow an ally unit. </summary>
	/// <description> The AI follows the ally 'target' while still looking for enemies.
	/// </description>
	//--------------------------------------
	void FollowingState()
	{
		// TODO: 1. For starters, implement as simply following a target (by trying to get to it's transform)
		//		 	For the purpose rewrite DetectEnemy so that it works for both enemies and allies (based on an argument)
		//			See ChasingState()

		//			Do DetectEnemy 
		//			If this AI detects an enemy change state to "CHASING" setting the new target to be the enemy

		// 			If the follow ally target gets out of range change to "STANDING"

		//throw new System.NotImplementedException ();
	}


	//--------------------------------------------------------------
	/// <summary> Keeps formation. </summary>
	/// <description> The AI considers nearby ally units and the enemy and keeps its formation
	/// relative to those units.
	/// </description>
	//--------------------------------------
	void KeepFormation()
	{
		// TODO: 2. The AI scans for nearby units and tries to stay a certain distance away from them

		//			For the purpose, ideally, you'll need to add 3D vector to keep track of the movement from
		//			all functions (e.g. aiControlVector) so that you can make a relative change to that
		//			and at the end of update do controller.Move( aiControlVector );

		//			Alternatively adjust the controller.relativeVelocity by a small factor (based on distance) 
		//			to make this unity avoid going into the units
		//			NOTE: You'll have to remove the "private" from "private set"

		//			This function can be improved to make the AI look like a professional football player
		//			and try to keep an actual formation (this is a bonus feature)

		
		//We need another controller function to add force onto the new velocity instead of completely erasing it
		//controller.Move( direction2D + new Vector2(avoidanceForce.x,avoidanceForce.z), false);

		//throw new System.NotImplementedException();
	}

	//--------------------------------------------------------------
	/// <summary> Detects the enemy. </summary>
	/// <returns>The enemy.</returns>
	//--------------------------------------
	BaseCharacterController DetectEnemy()
	{
		BaseCharacterController target = null;

		// if we hate the player - target him directly
		if( playerHate > Random.value )
		{
			target = Player.characterController;
		}
		else
		{
			Collider[] colliders = Physics.OverlapSphere( transform.position, alertRadius );

			foreach( Collider collider in colliders )
			{
				// If it is a different object
				if( collider.transform != transform )
				{
	                
					Faction otherFaction = collider.GetComponent<Faction>();
					BaseCharacterController otherController = collider.GetComponent<BaseCharacterController>();

	                if (!collider.GetComponent<BaseCharacterController>())
	                    continue;

					if( faction && faction.IsAlly(otherFaction) )
					   continue; // Skip this iteration, we ignore allies

					if( otherController )
					{
						if( otherController.isKnockedDown )
						{
							continue;
						}
						else
						{
							target = otherController;
							break;
						}
					}
				}
			}
		}

		return target;
	}
}

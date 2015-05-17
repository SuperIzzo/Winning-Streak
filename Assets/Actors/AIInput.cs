/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          AIInput.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    18-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
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
[AddComponentMenu("Winning Streak/Character/AI Input",100)]
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


    [SerializeField] float _alertRadius = 5.0f;
    [SerializeField] float _chaseOffDistance = 10.0f;
    [SerializeField] float _redirectionRate = 0.01f;
    [SerializeField] float _chaseGiveUpRate = 0.0001f;
    [SerializeField] float _tackleRange = 2.0f;
    [SerializeField] float _playerHate = 0.0f;


    //--------------------------------------------------------------
    #region Public properties
    //--------------------------------------
    /// <summary> The radius in which the AI will become alerted
    /// if an enemy is close. </summary>
    public float alertRadius
    {
        get { return _alertRadius; }
        set { _alertRadius = value; }
    }

	/// <summary> The maximal distance between a chasing AI and
	/// it's target, before the AI gives up.</summary>
	public float chaseOffDistance
    {
        get { return _chaseOffDistance; }
        set { _chaseOffDistance = value; }
    }

    /// <summary> The rate at which an unaware AI will be changing
    /// its direction </summary>
    public float redirectionRate
    {
        get { return _redirectionRate; }
        set { _redirectionRate = value; }
    }

    /// <summary> The rate at which a chasing AI will randomly
    /// decide to give up a chase. </summary>
    public float chaseGiveUpRate
    {
        get { return _chaseGiveUpRate; }
        set { _chaseGiveUpRate = value; }
    }

    /// <summary> The distance at which a chasing AI will attempt
    /// to a tackle. </summary>
    public float tackleRange
    {
        get { return _tackleRange; }
        set { _tackleRange = value; }
    }

    /// <summary> The rate at which the player will be targeted
    /// (unfairly) by the AI. </summary>
    public float playerHate
    {
        get { return _playerHate; }
        set { _playerHate = value; }
    }
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
	protected void Start() 
	{
		controller = GetComponent<BaseCharacterController>();
		faction = GetComponent<Faction>();
		state = AIStates.UNAWARE;
	}

    //--------------------------------------------------------------
    /// <summary> Update is called once per frame. </summary>
    //--------------------------------------
    protected void Update() 
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

		throw new System.NotImplementedException ();
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
			target = Player.p1.characterController;
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

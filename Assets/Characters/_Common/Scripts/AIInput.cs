using UnityEngine;
using System.Collections;

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
[RequireComponent(typeof(BaseCharacterController))]
public class AIInput : MonoBehaviour 
{
	private enum AIStates
	{
		CHASING,		// to tackle
		FOLLOWING,		// an ally
		STANDING,
	}

	public float alertRadius = 5.0f;
	public float chaseOffDistance = 10.0f; 
	public float redirectionRate = 0.01f;
	public float tackleRange	= 2.0f;

    public float tackleSpeed = 1;
    public float tackleDuration = 0.5f;

    //Time for the downed AI to slowly fade into the floor before being deleted
    public float fadeTime = 10;

    //Can only tackle once, stops the Coroutine from being played multiple times
    private bool tackled = false;


	// Internal links
	BaseCharacterController controller;
	Faction faction;

	// State
	AIStates state;
	Transform target;
	Vector2 roamingDirection;


	//--------------------------------------------------------------
	/// <summary> Use this for initialization. </summary>
	//--------------------------------------
	void Start () 
	{
		controller = GetComponent<BaseCharacterController>();
		faction = GetComponent<Faction>();
		state = AIStates.STANDING;
	}

	//--------------------------------------------------------------
	/// <summary> Update is called once per frame. </summary>
	//--------------------------------------
	void Update () 
	{
		switch( state )
		{
		case AIStates.STANDING:
			StandingState();
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
	void StandingState()
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

        if (Random.value < 0.01f)
        {
            BaseCharacterController enemy = DetectEnemy();
            if (enemy)
            {
                target = enemy.transform;
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
			Vector3 direction = target.position - transform.position;
			Vector2 direction2D = new Vector2( direction.x, direction.z ); 
			float distance = direction2D.magnitude;
			direction2D.Normalize();
			controller.Move( direction2D );

			if( distance > chaseOffDistance )
				state = AIStates.STANDING;

            if (distance < tackleRange)
            {
                controller.Tackle(true);

                if (!tackled)
                {
                    StartCoroutine("Dive");
                }
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


		//throw new System.NotImplementedException();
	}

	//--------------------------------------------------------------
	/// <summary> Detects the enemy. </summary>
	/// <returns>The enemy.</returns>
	//--------------------------------------
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

                if (!collider.GetComponent<BaseCharacterController>())
                    continue;

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

    IEnumerator Dive()
    {
		// UNGODLY HACK: Not only does this directly control the character animation, 
		//               but it also does it by directly modifying the game state
        float timer = 0;
        tackled = true;

        while (timer < tackleDuration)
        {
            timer += Time.unscaledDeltaTime;
            this.transform.position += transform.forward * (tackleSpeed * Time.unscaledDeltaTime);

            yield return null;
        }

        StartCoroutine("Death");
    }

    IEnumerator Death()
    {
        float timer = 0;

        while (timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}

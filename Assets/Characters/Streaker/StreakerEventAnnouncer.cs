using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BaseCharacterController))]
public class StreakerEventAnnouncer : MonoBehaviour
{
	BaseCharacterController controller;
	Commentator commentator;

	bool	pickedBallEvent 	= false;
	float	dancingNoticeTime	= 1.0f;
	float 	dancingCooldown		= 1.0f;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
		commentator = GameSystem.commentator;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( commentator )
		{
			if( controller )
			{
				if( controller.heldObject!=null && !pickedBallEvent )
				{
					pickedBallEvent = true;
					commentator.Comment( CommentatorEvent.PICKED_BALL );
				}
				else if( controller.heldObject==null )
				{
					pickedBallEvent = false;
				}

				if( controller.isDancing && dancingCooldown>0 )
				{
					dancingCooldown -=Time.deltaTime;
					if( dancingCooldown <=0 )
					{
						bool commented = commentator.Comment( CommentatorEvent.WIGGLE );

						// try again next frame
						if( !commented )
							dancingCooldown = 0.01f;
					}
				}
				else if( !controller.isDancing )
				{
					dancingCooldown = dancingNoticeTime;
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class TouchdownEvent : MonoBehaviour
{

	void OnTriggerEnter(Collider col)
	{
		if( col.gameObject.CompareTag( Tags.ball ) )
		{
			ThrowableObject throwable = col.GetComponent<ThrowableObject>();
			if( throwable!=null && throwable.isThrown )
			{
				Commentator commentator = Commentator.instance;

				if( commentator )
				{
					ScoreManager.AddScore(100);
					ScoreManager.AddMultPoint( 2 );
					commentator.Comment( CommentatorEvent.TOUCH_DOWN );
				}
			}
		}
	}
}

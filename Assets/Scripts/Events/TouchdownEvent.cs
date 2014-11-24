using UnityEngine;
using System.Collections;

public class TouchdownEvent : MonoBehaviour
{

	void OnTriggerEnter(Collider col)
	{
		if( col.gameObject.CompareTag( "ball" ) )
		{
			Throwable throwable = col.GetComponent<Throwable>();
			if( throwable!=null && throwable.isThrown )
			{
				Commentator commentator = Commentator.GetInstance();

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

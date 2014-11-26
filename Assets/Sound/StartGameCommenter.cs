using UnityEngine;
using System.Collections;

public class StartGameCommenter : MonoBehaviour
{
	public static bool announced = false;


	int frametimer;

	// Use this for initialization
	void Start ()
	{
		frametimer = 5;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( !announced )
		{
			frametimer--;

			if( frametimer<=0 )
			{
				Commentator commentator = Commentator.GetInstance();

				if( commentator )
					announced = commentator.Comment( CommentatorEvent.GAME_START );
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class StartGameCommenter : MonoBehaviour
{
	public static bool announced = true;


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
				announced = true;
				Commentator commentator = Commentator.GetInstance();

				if( commentator )
					commentator.Comment( CommentatorEvent.GAME_START );
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class RandomCommentsGenerator : MonoBehaviour
{
	public float minSilence = 10.0f;
	public float maxSilence = 20.0f;

	public float commentCountDown = 0;

	Commentator commentator;

	// Use this for initialization
	void Start ()
	{
		commentator = Commentator.GetInstance();
	}
	
	// Update is called once per frame
	void Update ()
	{
		commentCountDown -= Time.unscaledDeltaTime;

		if( commentCountDown < 0 )
		{
			commentCountDown = Random.Range(minSilence, maxSilence);
			commentator.Comment( CommentatorEvent.RANDOM );
		}
	}
}

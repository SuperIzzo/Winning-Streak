using UnityEngine;
using System.Collections;

public class RandomCommentsGenerator : MonoBehaviour
{
	public float minSilence = 5.0f;
	public float maxSilence = 10.0f;

	private float silenceTime = 0;

	Commentator commentator;

	// Use this for initialization
	void Start ()
	{
		commentator = GameSystem.commentator;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Time.unscaledTime - commentator.timeSinceLastComment > silenceTime )
		{
			silenceTime = Random.Range(minSilence, maxSilence);
			commentator.Comment( CommentatorEvent.RANDOM );
		}
	}
}

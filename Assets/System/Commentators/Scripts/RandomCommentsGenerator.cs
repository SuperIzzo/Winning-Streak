using UnityEngine;
using System.Collections;

public class RandomCommentsGenerator : MonoBehaviour
{
	public float minSilence = 5.0f;
	public float maxSilence = 10.0f;

	private float silenceTime = 0;

	// Update is called once per frame
	void Update ()
	{
		if( Time.unscaledTime - Commentator.timeSinceLastComment > silenceTime )
		{
			silenceTime = Random.Range(minSilence, maxSilence);
			Commentator.Comment( CommentatorEvent.RANDOM );
		}
	}
}

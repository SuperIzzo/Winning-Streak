using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum CommentatorEvent
{
	NONE = 0,
	RANDOM,
	PICKED_BALL,
	DODGE_TACKLE
}


[System.Serializable]
public class CommentatorQueue
{
	public string name;
	public CommentatorEvent commentEvent;
	public List<AudioClip> clipQueue;
}


public class Commentator : MonoBehaviour
{
	// Public / data
	public List<CommentatorQueue> commentQueues;

	// Internal / state
	static Commentator instance;
	CommentatorQueue currentQueue;
	int clipIndex = 0;


	// Returns a single instance for this component
	public static Commentator GetInstance()
	{
		if( !instance )
		{
			instance = GameObject.FindObjectOfType<Commentator>();
		}

		return instance;
	}


	// Public interface
	public bool Comment( CommentatorEvent evt )
	{
		// Pick a random comment which matches the event
		int start = Random.Range(0, commentQueues.Count );
		int i = start+1;

		// repeat until we've cycled them all
		while( i!=start )
		{
			// Wrap around if we have to
			i = i % commentQueues.Count;

			CommentatorQueue commentQueue = commentQueues[i];
			if( commentQueue !=null
			    && commentQueue.commentEvent == evt 
			    && commentQueue.clipQueue !=null
			    && commentQueue.clipQueue.Count>0 )
			{
				return PlayeQueue( commentQueue );
			}

			// to the next comment
			i++;
		}

		return false;
	}


	// Sets the current queue
	private bool PlayeQueue( CommentatorQueue queue )
	{
		// TODO: check event priority
		clipIndex = 0;
		currentQueue = queue;

		return true;
	}


	// Updates the Commentator
	void Update()
	{
		// If teh audio is not playing move onto the
		// the next line in the script (if any left)
		if( !audio.isPlaying )
		{
			if( currentQueue != null && clipIndex < currentQueue.clipQueue.Count-1 )
			{
				clipIndex++;
				AudioClip clip = currentQueue.clipQueue[ clipIndex ];
				if( clip )
				{
					audio.clip = clip;
					audio.Play();
				}
			}
		}
	}
}

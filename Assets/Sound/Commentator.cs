using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum CommentatorEvent
{
	NONE = 0,
	RANDOM,
	PICKED_BALL,
	DODGE_TACKLE,
	GAME_START,
	GAME_OVER,
	HIT_PLAYER,
	WIGGLE,
	TOUCH_DOWN,
	PTS_1,
	PTS_2,
	PTS_3,
	PTS_4,
	PTS_5,
	PTS_6,
	PTS_7,
	PTS_8,
	PTS_9,
	PTS_10,
	SCORED_GOAL
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
	
	public float timeSinceLastComment;
	
	
	// Returns a single instance for this component
	public static Commentator GetInstance()
	{
		if( !instance )
		{
			instance = GameObject.FindObjectOfType<Commentator>();
		}
		
		return instance;
	}
	
	public static int GetEventPriority( CommentatorEvent ev )
	{
		switch( ev )
		{
		case CommentatorEvent.RANDOM:
			return 0;
		case CommentatorEvent.GAME_START:
			return 1;
		case CommentatorEvent.SCORED_GOAL:
			return 3;
		case CommentatorEvent.GAME_OVER:
			return 10;
		default:
			return 2;
		}
	}
	
	// Public interface
	public bool Comment( CommentatorEvent evt )
	{
		if( currentQueue!=null )
		{
			int currentPriority = GetEventPriority( currentQueue.commentEvent );
			int newPriority = GetEventPriority( evt );
			
			// Lower priority events are ignored
			if( newPriority <= currentPriority )
				return false;
		}
		
		// Pick a random comment which matches the event
		int start = Random.Range(0, commentQueues.Count );
		int step  = Random.Range(1, commentQueues.Count-1);
		int i = start+1;
		
		// repeat until we've cycled them all
		for( int j=0; j<commentQueues.Count; j++ )
		{
			// End condition
			if( i==start )
				break;
			
			// Wrap around if we have to
			i = i % commentQueues.Count;
			
			CommentatorQueue commentQueue = commentQueues[i];
			if( commentQueue !=null
			   && commentQueue.commentEvent == evt 
			   && commentQueue.clipQueue !=null
			   && commentQueue.clipQueue.Count>0 )
			{
				//Debug.Log( "Commenting: " + commentQueue.name );
				return PlayeQueue( commentQueue );
			}
			
			// to the next comment
			i += step;
		}
		
		return false;
	}
	
	
	// Sets the current queue
	private bool PlayeQueue( CommentatorQueue queue )
	{
		clipIndex = 0;
		currentQueue = queue;
		
		// Stop the current commentating event
		audio.Stop();
		
		return true;
	}
	
	// Start the commentators
	void Start()
	{
		timeSinceLastComment = Time.unscaledTime;
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
				AudioClip clip = currentQueue.clipQueue[ clipIndex ];
				
				if( clip )
				{
					audio.clip = clip;
					audio.Play();
					
					timeSinceLastComment = Time.unscaledTime;
				}
				
				clipIndex++;
			}
			else
			{
				currentQueue = null;
			}
		}
	}
}

//#define DEBUG_COMMENTS

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--------------------------------------------------------------
/// <summary> An enumerationn of all commentator event. </summary>
//--------------------------------------
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


//--------------------------------------------------------------
/// <summary> A commentator conversation queue. </summary>
//-------------------------------------- 
[System.Serializable]
public class CommentatorQueue
{
	public string name;
	public CommentatorEvent commentEvent;
	public List<AudioClip> clipQueue;
}


//--------------------------------------------------------------
/// <summary> Commentator. </summary>
//--------------------------------------
public class Commentator : MonoBehaviour
{
	#region Public settings
	public List<CommentatorQueue> commentQueues;
	#endregion


	#region Public properties
	public float timeSinceLastComment {get; private set;}
	#endregion


	#region Private state
	CommentatorQueue currentQueue;
	int clipIndex = 0;
	#endregion

	//--------------------------------------------------------------
	/// <summary> Returns the relative priority of the  
	/// commentator event. </summary>
	/// <description>
	/// The numbers returned are arbitrary and signify the importance
	/// of the event in relation to other events. More important
	/// events will cut-off less important ones when a 
	/// confict arises. The higher the number the higher the priority
	/// of the event.
	/// </description>
	/// <returns>The event priority.</returns>
	/// <param name="ev">the event</param>
	//--------------------------------------
	public static int GetEventPriority( CommentatorEvent ev )
	{
		switch( ev )
		{
		case CommentatorEvent.PTS_1:
			return 1;
		case CommentatorEvent.PTS_2:
			return 1;
		case CommentatorEvent.PTS_3:
			return 2;
		case CommentatorEvent.PTS_4:
			return 2;
		case CommentatorEvent.PTS_5:
			return 3;
		case CommentatorEvent.PTS_6:
			return 3;
		case CommentatorEvent.PTS_7:
			return 3;
		case CommentatorEvent.PTS_8:
			return 4;
		case CommentatorEvent.PTS_9:
			return 4;
		case CommentatorEvent.PTS_10:
			return 4;
			
		case CommentatorEvent.RANDOM:
			return 2;
		case CommentatorEvent.PICKED_BALL:
			return 2;
		case CommentatorEvent.WIGGLE:
			return 3;
		case CommentatorEvent.GAME_START:
			return 4;
		case CommentatorEvent.SCORED_GOAL:
			return 4;
		case CommentatorEvent.TOUCH_DOWN:
			return 4;
		case CommentatorEvent.GAME_OVER:
			return 10;
		default:
			return 1;
		}
	}

	//--------------------------------------------------------------
	/// <summary> Play a random comment of the specified 
	/// <see cref="CommentatorEvent"/> </summary>
	/// <description>
	/// The call will fail if a comment queue for the specified 
	/// <see cref="CommentatorEvent"/> could not be found or a 
	/// higher priority comment is currently playing, returning 
	/// <c>false</c>.
	/// </description>
	/// <param name="evt">the commentator event.</param>
	/// <returns> Returns <c>true</c> if a comment was successfully 
	/// queued; Otherwise <c>false</c>.</returns>
	//--------------------------------------
	public bool Comment( CommentatorEvent evt )
	{
		#if DEBUG_COMMENTS
		Debug.Log("Commentating Event: " + evt);
		#endif
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
				#if DEBUG_COMMENTS
				Debug.Log( "Commenting: " + commentQueue.name );
				#endif
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
			if( currentQueue != null && clipIndex < currentQueue.clipQueue.Count )
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

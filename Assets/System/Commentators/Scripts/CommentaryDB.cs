using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CommentaryDB : ScriptableObject
{
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

	#region Public settings
	public List<CommentatorQueue> commentQueues;
	#endregion
}

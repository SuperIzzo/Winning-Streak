using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Difficulty Manager manages the difficulty level. </summary>
//-------------------------------------- 
public class DifficultyManager : MonoBehaviour
{
	//--------------------------------------------------------------
	#region  Public settings
	//--------------------------------------
	/// <summary> The difficulty increment per second. </summary>
	public float incPerSecond = 0.001f;
	#endregion


	//--------------------------------------------------------------
	#region  Public properties
	//--------------------------------------
	public float level
	{ 
		get{return _level;}
		private set{ _level = Mathf.Clamp01(value);}
	}
	#endregion

	
	//--------------------------------------------------------------
	#region Private state
	//--------------------------------------
	private float _level = 0;
	#endregion


	//--------------------------------------------------------------
	/// <summary> Update the difficulty level. </summary>
	//--------------------------------------
	void Update ()
	{
		// Note that the difficulty increases independantly
		// from the timeScale, this make slo-mo a double-edged
		// knife because frequent use will make the game difficult
		// much faster denying the players scoring opportunities
		// Also, just to be fair it doesn't run when the game is paused
		if( Time.timeScale>float.Epsilon )
		{
			level += incPerSecond * Time.unscaledDeltaTime;
		}
	}
}

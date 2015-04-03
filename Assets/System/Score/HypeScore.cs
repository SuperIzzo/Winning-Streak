using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Hype score. </summary>
//--------------------------------------
[RequireComponent(typeof(Score))]
public class HypeScore : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Reference to score </summary>
	//--------------------------------------
	private Score score;

	//--------------------------------------------------------------
	/// <summary> Initialises this instance. </summary>
	//--------------------------------------
	void Start ()
	{
		score = GetComponent<Score>();
	}
	
	//--------------------------------------------------------------
	/// <summary> Updates this instance. </summary>
	//--------------------------------------
	void Update ()
	{
		score.baseScore +=  Time.deltaTime * GameSystem.crowd.hype;
	}
}

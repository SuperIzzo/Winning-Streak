using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent( typeof(Animator) )]
public class HUD : MonoBehaviour
{
	public float startupShowTime = 5.0f;
	public Text scoreText;
	public Text timeText;

	private float startTime = 0;


	private Animator animator;


	// Use this for initialization
	void Start ()
	{
		startTime = Time.time;	
		animator = GetComponent<Animator>();
		Invoke("Show", startupShowTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		scoreText.text = "SCORE: " + ScoreManager.baseScore +"\tx" + ScoreManager.multPoints;

		float timeSinceStart = Time.time - startTime;
		int seconds = Mathf.FloorToInt(timeSinceStart) % 60;
		int minutes = Mathf.FloorToInt(timeSinceStart) / 60;

		string timeStr = "";

		if( minutes < 10 )
			timeStr += "0";
		timeStr += minutes + ":";

		if( seconds < 10 )
			timeStr += "0";
		timeStr += seconds;

		timeText.text = timeStr;
	}

	// Shows the HUD
	public void Show()
	{
		animator.SetBool("visible", true);
	}

	// Hides the HUD
	public void Hide()
	{
		animator.SetBool("visible", false);
	}
}

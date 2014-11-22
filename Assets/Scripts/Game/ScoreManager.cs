using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public static int score = 0;
	static bool isActive = true;

	float timer = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevelName != "main-2" || !isActive)
			return;

		timer += Time.deltaTime;

		if(timer > 0.1f)
		{
			score++;
			timer = 0;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,50), "Score: " + score);
	}

	public static void AddScore(int toAdd)
	{
		score += toAdd;
	}

	public static void StartTimer()
	{
		score = 0;
		isActive = true;
	}

	public static void StopTimer()
	{
		score = 0;
		isActive = false;
	}
}

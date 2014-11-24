using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{

	public static int score
	{
		get{ return (int)(baseScore * multPoints); }
	}

	public static float baseScore = 0; 
	public static float multPoints = 1;
	static bool isActive = true;

	float timer = 0;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Application.loadedLevelName != "main-2" || !isActive)
			return;

		timer += Time.deltaTime;

		if(timer > 0.1f)
		{
			baseScore++;
			timer = 0;
		}
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10,200,50), "Score: " + ((int)baseScore) + " x" + ((int)multPoints) );
	}

	public static void AddScore(float toAdd)
	{
		baseScore += toAdd;
	}
	
	public static void AddMultPoint( float multPts )
	{
		multPoints += multPts;
	}

	public static void StartTimer()
	{
		baseScore = 0;
		multPoints = 0;
		isActive = true;
	}

	public static void StopTimer()
	{
		baseScore = 0;
		multPoints = 0;
		isActive = false;
	}
}

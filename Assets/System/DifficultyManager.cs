using UnityEngine;
using System.Collections;

public class DifficultyManager : MonoBehaviour
{
	private float _level = 0;

	public float level
	{
		get
		{
			return _level;
		}

		private set
		{
			_level = Mathf.Clamp01(value);
		}
	}

	public float incPerSecond = 0.001f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Note that the difficulty increases independantly
		// from the timeScale, this make slo-mo a double-edged
		// knife because frequent use will make the game difficult
		// much faster denying the players scoring opportunities
		// Also, to be fair it doesn't run when the game is paused
		if( Time.timeScale>float.Epsilon )
		{
			level += incPerSecond * Time.unscaledDeltaTime;
		}
	}
}

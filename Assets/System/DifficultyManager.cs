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
		level += incPerSecond * Time.unscaledDeltaTime;
	}
}

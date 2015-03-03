using UnityEngine;
using System.Collections;

public class TimeFlow : MonoBehaviour
{
	private float defaultFixedDelta = 0.02f;

	private bool _isSlowed = false;
	public  bool isSlowed
	{
		get { return _isSlowed; }
		set { _isSlowed = value; UpdateTimeScale(); }
	}

	private bool _isPaused = false;
	public  bool isPaused
	{
		get { return _isPaused; }
		set { _isPaused = value; UpdateTimeScale(); }
	}



	void Start()
	{
		defaultFixedDelta = Time.fixedDeltaTime;
	}

	void UpdateTimeScale()
	{
		float timeScale = 1.0f;

		if( _isPaused )
			timeScale = 0.0f;
		else if( _isSlowed )
			timeScale = 0.2f;

		Time.timeScale = timeScale;
		//Time.fixedDeltaTime = defaultFixedDelta * Time.timeScale;
	}
}

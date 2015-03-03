using UnityEngine;
using System.Collections;

public class CameraMode : MonoBehaviour
{
	public MonoBehaviour[] modes;

	private int currentMode = 0;

	// Use this for initialization
	void Start ()
	{
		UpdateModes();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( Input.GetButtonDown( "CameraMode" ) )
		{
			NextMode();
		}
	}

	void NextMode()
	{
		do
		{
			currentMode++;
			currentMode %= modes.Length;
		}
		while( modes[currentMode]== null );

		UpdateModes();
	}

	void UpdateModes()
	{
		foreach( MonoBehaviour mode in modes )
		{
			mode.enabled = false;
		}

		modes[currentMode].enabled = true;
	}
}

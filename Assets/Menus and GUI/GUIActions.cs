using UnityEngine;
using System.Collections;

public class GUIActions : MonoBehaviour
{
	public void RestartLevel()
	{
		Application.LoadLevel( Application.loadedLevel );
	}

	public void ChangeLevel( string levelName )
	{
		Application.LoadLevel( levelName );
	}
}

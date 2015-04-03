using UnityEngine;
using System.Collections;

public class GameSession : MonoBehaviour
{
	public static GameSession current
	{
		get
		{
			if( !_current )
			{
				_current = GameObject.FindObjectOfType<GameSession>();
			}
			
			return _current;
		}
	}
	private static GameSession _current;


	public float timePlayed {get; private set;}

	// Use this for initialization
	void Start ()
	{
		timePlayed = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timePlayed += Time.deltaTime;
	}
}

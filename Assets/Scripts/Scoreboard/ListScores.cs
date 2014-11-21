using UnityEngine;
using System.Collections;

public class ListScores : MonoBehaviour {

	public GameObject text;
	GameObject gameManager;

	// Use this for initialization
	void Start () 
	{
		gameManager = GameObject.Find("_GameManager");
	}
	

}

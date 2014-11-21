using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	Vector3 lookatOffset = new Vector3();
	Vector3 positionOffset = new Vector3();

	void Start () 
	{
		
	}

	void Update () 
	{
		this.transform.LookAt(player.transform.position);
	}
}

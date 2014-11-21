using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	public Vector3 lookatOffset = new Vector3();
	public Vector3 positionOffset = new Vector3();

	public float followSpeed = 6;

	void Start () 
	{
		
	}

	void Update () 
	{
		this.transform.LookAt(player.transform.position + lookatOffset);

		Vector3 newPos = player.transform.position + positionOffset;

		this.transform.position = Vector3.Lerp(this.transform.position,newPos,Time.deltaTime * followSpeed);
	}
}

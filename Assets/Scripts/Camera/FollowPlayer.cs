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

		//side to side tracking
		Vector3 newPos = new Vector3(player.transform.position.x,positionOffset.y,positionOffset.z);
		this.transform.position = Vector3.Lerp(this.transform.position,newPos,Time.deltaTime * followSpeed);

		//fov tracking
		Vector3 invertedCamera = new Vector3(this.transform.position.x, this.transform.position.y,
		                                     -this.transform.position.z - 10);

		this.camera.fieldOfView = Vector3.Distance(player.transform.position,invertedCamera);


	}
}

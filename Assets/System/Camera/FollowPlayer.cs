using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	public Vector3 lookatOffset = new Vector3();
	public Vector3 positionOffset = new Vector3();

	Vector3 lastLookAt;
	Vector3 lookAtNow;

	float lookTimer = 0;

	public float followSpeed = 6;
	public float dofMultiplier = 1;

	void Start () 
	{
		
	}

	void Update () 
	{
		lookTimer += Time.deltaTime;

		if(lookTimer > 1)
		{
			lastLookAt = lookAtNow;
			lookAtNow = player.transform.position + lookatOffset;


			lookTimer =0;
		}

		if( Input.GetKey(KeyCode.PageUp) )
		{
			positionOffset.z += Time.unscaledDeltaTime * 2;
		}

		if( Input.GetKey(KeyCode.PageDown ) )
		{
			positionOffset.z -= Time.unscaledDeltaTime * 2;
		}

		if( Input.GetKey(KeyCode.Home) )
		{
			dofMultiplier -= Time.unscaledDeltaTime/2;
		}
		
		if( Input.GetKey(KeyCode.End ) )
		{
			dofMultiplier += Time.unscaledDeltaTime/2;
		}

		if( Input.GetKey( KeyCode.Insert ) )
		{
			followSpeed += Time.unscaledDeltaTime*2;
		}

		if( Input.GetKey( KeyCode.Delete ) )
		{
			followSpeed -= Time.unscaledDeltaTime*2;
		}

		//this.transform.LookAt(Vector3.Lerp(lastLookAt,lookAtNow,Time.deltaTime));

		Quaternion targetRot = Quaternion.LookRotation(player.transform.position - this.transform.position);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.deltaTime);

		//side to side tracking
		Vector3 newPos = new Vector3(player.transform.position.x,positionOffset.y,positionOffset.z);
		this.transform.position = Vector3.Lerp(this.transform.position,newPos,Time.deltaTime * followSpeed);

		//fov tracking
		Vector3 invertedCamera = new Vector3(this.transform.position.x, this.transform.position.y,
		                                     -this.transform.position.z - 10);

		this.camera.fieldOfView = Vector3.Distance(player.transform.position,invertedCamera) * dofMultiplier;


	}
}

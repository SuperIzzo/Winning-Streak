using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

	public enum CameraMode 
	{
		FOLLOW,
		FLY
	}

	public GameObject player;

	public Vector3 lookatOffset = new Vector3();
	public Vector3 positionOffset = new Vector3();

	public float followSpeed = 6;
	public float dofMultiplier = 1;

	public CameraMode cameraMode = CameraMode.FOLLOW;
	public bool moveCamera = true;
	public MonoBehaviour mouseLook = null;

	void Start () 
	{
		
	}

	void Update () 
	{
		if( Input.GetKeyDown(KeyCode.V) )
		{
			moveCamera = !moveCamera;
		}

		if( Input.GetKeyDown(KeyCode.B) )
		{
			if( cameraMode == CameraMode.FOLLOW )
				cameraMode = CameraMode.FLY;
			else
			{
				cameraMode = CameraMode.FOLLOW;
				positionOffset = transform.position - player.transform.position;
			}
		}

		if( mouseLook )
		{
			mouseLook.enabled = (cameraMode == CameraMode.FLY && moveCamera);
		}

		if( cameraMode != CameraMode.FOLLOW || !moveCamera)
		{
			return;
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

		Quaternion targetRot = Quaternion.LookRotation(player.transform.position - this.transform.position);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.unscaledDeltaTime);

		//side to side tracking
		Vector3 newPos = new Vector3(player.transform.position.x + positionOffset.x,
		                             player.transform.position.y + positionOffset.y,
		                             player.transform.position.z + positionOffset.z);
		this.transform.position = Vector3.Lerp(this.transform.position,newPos,Time.unscaledDeltaTime * followSpeed);

		//fov tracking
		Vector3 invertedCamera = new Vector3(this.transform.position.x, this.transform.position.y,
		                                     -this.transform.position.z - 10);

		this.camera.fieldOfView = Vector3.Distance(player.transform.position,invertedCamera) * dofMultiplier;


	}
}

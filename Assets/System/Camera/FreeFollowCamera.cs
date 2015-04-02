using UnityEngine;
using System.Collections;

public class FreeFollowCamera : MonoBehaviour
{
	public FollowCameraConfig config;

	private Transform target;

	void Start ()
	{
		target = Player.p1.transform;
	}

	void OnEnable()
	{
		if( !target )
			target = Player.p1.transform;

		config.positionOffset = transform.position - target.position;

		Vector3 targetDir = transform.forward * config.positionOffset.magnitude;
		config.lookAtOffset = (transform.position + targetDir) - target.position;
	}

	// Use this for initialization
	void Update ()
	{
		Quaternion targetRot = Quaternion.LookRotation((target.position + config.lookAtOffset) - this.transform.position);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.unscaledDeltaTime * config.turnSpeed);
		
		//side to side tracking
		Vector3 newPos = target.position + config.positionOffset;
		this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.unscaledDeltaTime * config.followSpeed);
	}
}

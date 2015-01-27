﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class FollowCameraConfig
{
	public Vector3 lookAtOffset = new Vector3();
	public Vector3 positionOffset = new Vector3();
	public float followSpeed = 6;
	public float turnSpeed = 1;
	public float FOVModifier= 1;
}

public class FollowTarget : MonoBehaviour
{

	private Transform target;

	public FollowCameraConfig normal;
	public FollowCameraConfig zoomed;

	private FollowCameraConfig currentConfig;

	void Start () 
	{
		currentConfig = normal;
	}

	void Update () 
	{
        if (target)
        {
            Quaternion targetRot = Quaternion.LookRotation(target.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRot, Time.deltaTime * currentConfig.turnSpeed);

            //side to side tracking
            Vector3 newPos = new Vector3(target.position.x, currentConfig.positionOffset.y, currentConfig.positionOffset.z);
            this.transform.position = Vector3.Lerp(this.transform.position, newPos, Time.deltaTime * currentConfig.followSpeed);

            //fov tracking
            Vector3 invertedCamera = new Vector3(this.transform.position.x, this.transform.position.y,
                                                 -this.transform.position.z - 10);

            camera.fieldOfView = Vector3.Distance(target.position, invertedCamera) * currentConfig.FOVModifier;
        }
        else
        {
			target = Player.transform;
        }
	}

	public void ZoomIn()
	{
		currentConfig = zoomed;
	}

	public void ZoomReset()
	{
		currentConfig = normal;
	}
}

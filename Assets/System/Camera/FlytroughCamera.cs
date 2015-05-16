/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      FlytroughCamera.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    03-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class FlytroughCamera : MonoBehaviour
{
	public float lookSpeed = 15.0f;
	public float moveSpeed = 15.0f;
	
	public float rotationX = 0.0f;
	public float rotationY = 0.0f;

	private bool mouseLook = true;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{			
		rotationX += Input.GetAxis("Mouse X")*lookSpeed;
		rotationY += Input.GetAxis("Mouse Y")*lookSpeed;

		// Joystic axis
		float rsx = Input.GetAxis("RightStick X");
		float rsy = Input.GetAxis("RightStick Y");
		if( Mathf.Abs(rsx)<0.5f ) rsx = 0;
		if( Mathf.Abs(rsy)<0.5f ) rsy = 0;
		Debug.Log( "Joystic look axes: " + rsx +", " + rsy );
		rotationX += rsx*lookSpeed;
		rotationY += rsy*lookSpeed;

		rotationY = Mathf.Clamp (rotationY, -90, 90);

		transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
		transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

		float speedUp = 1;
		if( Input.GetButton("Dash") || Input.GetAxis("Dash") > 0.5 )
			speedUp = 5; 

		transform.position += transform.forward*moveSpeed*speedUp*Input.GetAxisRaw("Vertical");
		transform.position += transform.right*moveSpeed*speedUp*Input.GetAxisRaw("Horizontal");
	}
}
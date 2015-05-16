/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      CensorBarLookAt.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    27-Jan-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class CensorBarLookAt : MonoBehaviour
{
	public Camera _camera;

	// Use this for initialization
	void Start ()
	{
		if( !_camera )
		{
			_camera = Camera.main;
		}
	}
	
	// Update is called once per frame
	void Update () {

        if( _camera )
        {
            Quaternion rot = this.transform.rotation;
            Transform lookat = this.transform;

            lookat.LookAt(_camera.transform);

            rot.y = lookat.rotation.y;

            this.transform.rotation = rot;
        }
	}
}

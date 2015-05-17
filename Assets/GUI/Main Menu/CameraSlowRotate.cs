/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     CameraSlowRotate.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    04-Apr-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class CameraSlowRotate : MonoBehaviour {
    public float rotateSpeed = 1;

    private Vector3 rotateDirection;

    private float delay = 1, timer = 0;
    bool rotate = false;

    void Start()
    {
        rotateDirection = new Vector3(0, Random.Range(0, 1), 0);

        if (rotateDirection.y == 0)
            rotateDirection.y = -1;
    }

	void Update () 
    {
        if (rotate)
            this.gameObject.transform.Rotate(rotateDirection, rotateSpeed * Time.deltaTime);
        else
        {
            timer += Time.deltaTime;

            if (timer > delay)
                rotate = true;
        }
	}
}

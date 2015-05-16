/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        DebugCamera.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    06-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class DebugCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    bool frozen = false;
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.Keypad0) && !frozen)
        {
            Debug.Log("Freezing");
            Time.timeScale = 0;
            frozen = true;

            return;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && frozen)
        {
            Debug.Log("Un-freezing");
            //Time.timeScale = 1;
            frozen = false;

            return;
        }
        
	}
}

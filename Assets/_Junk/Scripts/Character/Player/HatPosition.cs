/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        HatPosition.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    22-Nov-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class HatPosition : MonoBehaviour {

	public GameObject toFollow;
	

	// Use this for initialization
	void Start () {

		this.transform.position = toFollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//this.transform.localScale = originalScale;
		//this.transform.rotation = originalRotation;
		this.transform.position = toFollow.transform.position;


	}
}

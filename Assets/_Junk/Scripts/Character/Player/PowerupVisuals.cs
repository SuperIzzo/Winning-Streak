/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      PowerupVisuals.cs                      </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    21-Nov-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class PowerupVisuals : MonoBehaviour {

	public GameObject lowBar, midBar, highBar;

	void Update()
	{
		Vector3 playerPos = Player.p1.transform.position; 
		this.transform.position = playerPos + new Vector3(0, 1.15f, -0.5f);
	}

	public void EnableLow()
	{
		lowBar.SetActive(true);
	}

	public void EnableMid()
	{
		midBar.SetActive(true);
	}

	public void EnableHigh()
	{
		highBar.SetActive(true);
	}

	public void Shutdown()
	{
		lowBar.SetActive(false);
		midBar.SetActive(false);
		highBar.SetActive(false);
	}
}

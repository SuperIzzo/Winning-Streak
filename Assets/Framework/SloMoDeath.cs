/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        SloMoDeath.cs                        </file> * 
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

public class SloMoDeath : MonoBehaviour
{
	BaseCharacterController character;

	// Use this for initialization
	void Start ()
	{
		character = GetComponent<BaseCharacterController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( character.isKnockedDown )
		{
		}
	}
}

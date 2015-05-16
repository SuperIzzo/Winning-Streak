/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        Achievement.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    15-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

[System.Serializable]
public class Achievement
{
    public int id;
	public string name;
	public string description;
    public string texture;
	public Sprite icon;

    public bool unlocked = false;
}

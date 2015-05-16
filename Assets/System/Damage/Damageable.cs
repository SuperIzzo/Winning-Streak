/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        Damageable.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    13-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;


//--------------------------------------------------------------
/// <summary> An abstract damageable class. </summary>
//--------------------------------------
public abstract class Damageable : MonoBehaviour
{
	//--------------------------------------------------------------
	/// <summary> Callback that raises the damage event. </summary>
	/// <param name="damager">The <see cref="Damager"/> 
	/// that cause the damage.</param>
	//--------------------------------------
	public abstract void OnDamage( DamageInfo info );
}

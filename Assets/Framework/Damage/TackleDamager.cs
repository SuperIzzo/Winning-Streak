/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       TackleDamager.cs                      </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    //--------------------------------------------------------------
    /// <summary> Tackle damager that damages only while a 
    /// <see cref="BaseCharacterController"/> is tackling. </summary>
    /// <remarks> This components should be attached to the
    /// damaging parts of a character. </remarks>
    //--------------------------------------
    [AddComponentMenu("Damage/Tackle Damager",102)]
    public class TackleDamager : Damager
    {
	    public BaseCharacterController controller;

	    //--------------------------------------------------------------
	    /// <summary> Setup this instance. </summary>
	    //--------------------------------------
	    void Start()
	    {
		    if( controller==null )
		    {
			    controller = GetComponentInParent<BaseCharacterController>();
		    }
	    }

	    //--------------------------------------------------------------
	    /// <summary> Does damage calculations and calls back on the damagees. </summary>
	    /// <description> 
	    /// Damages any <see cref="Damageable"/> object that
	    /// this <see cref="Collider"/> collides/triggers but only while the 
	    /// target <see cref="BaseCharacterController"/> is tackling. 
	    /// </description> 
	    /// <param name="damageable"> the damageable to be damaged </param>
	    //--------------------------------------
	    public override bool DamageTest( Damageable damageable )
	    {
		    bool doDamage = false;

		    if( controller.isTackling )
		    {
			    // Do damage when tackling
			    doDamage = true;

			    // ...except when we are hitting ourselves
			    Damageable[] myDamageableParts= GetComponentsInParent<Damageable>();
			    foreach( Damageable myDamageable in myDamageableParts )
			    {
				    if( damageable == myDamageable )
				    {
					    doDamage = false;
					    break;
				    }
			    }
		    }

		    return doDamage;
	    }
    }
}
/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          Faction.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    17-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    //--------------------------------------------------------------
    /// <summary> Game faction id. </summary>
    //--------------------------------------
    public enum FactionID
    {
        STREAKER,
        TEAM_A,
        TEAM_B
    }

    //--------------------------------------------------------------
    /// <summary> Faction contains information and functions
    /// related to ingame teams. </summary>
    //--------------------------------------
    public class Faction : MonoBehaviour
    {
        /// <summary> The unique id of the faction. </summary>
        public FactionID id;

        /// <summary> Returns whether this factions and another faction
        /// identified by <see cref="FactionID"/> are in alliance. </summary>
        /// <returns><c>true</c> if this instance is an ally of the 
        /// specified otherFaction; otherwise, <c>false</c>.</returns>
        /// <param name="otherFaction">The other faction.</param>
        public bool IsAlly(FactionID otherFaction)
        {
            return this.id == otherFaction;
        }

        /// <summary> Returns whether this faction and another
        /// are in alliance </summary>
        /// <returns><c>true</c> if this instance is ally of the specified 
        /// otherFaction; otherwise, <c>false</c>.</returns>
        /// <param name="otherFaction">The other faction.</param>
        public bool IsAlly(Faction otherFaction)
        {
            if (otherFaction)
                return IsAlly(otherFaction.id);
            else
                return false;
        }

        /// <summary> Returns whether this faction and another faction
        /// identified by <see cref="FactionID"/> are enemies. </summary>
        /// <returns><c>true</c> if this instance is an enemy of the 
        /// specified otherFaction; otherwise, <c>false</c>.</returns>
        /// <param name="otherFaction">The other faction.</param>
        public bool IsEnemy(FactionID otherFaction)
        {
            return !IsAlly(otherFaction);
        }

        /// <summary> Returns whether this faction and another
        /// are enemies </summary>
        /// <returns><c>true</c> if this instance is an enemy of the
        /// specified otherFaction; otherwise, <c>false</c>.</returns>
        /// <param name="otherFaction">The other faction.</param>
        public bool IsEnemy(Faction otherFaction)
        {
            if (otherFaction)
                return IsEnemy(otherFaction.id);
            else
                return true;
        }
    }
}

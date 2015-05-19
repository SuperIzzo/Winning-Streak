/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       LPUserProfile.cs                      </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    04-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.SocialPlatforms.LocalImpl
{
    using UnityEngine;
    using UnityEngine.SocialPlatforms;



    class UserProfile : IUserProfile
    {
        // Public properties
        public string userName { get; protected set; }
        public string id { get; protected set; }
        public bool isFriend { get; protected set; }
        public UserState state { get; protected set; }
        public Texture2D image { get; protected set; }

        public UserProfile()
        {
            userName = "";
            id = "";
            isFriend = false;
            state = UserState.Offline;
            image = null;
        }
    }
}

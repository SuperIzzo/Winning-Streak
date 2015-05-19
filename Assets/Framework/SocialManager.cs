/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                       SocialManager.cs                      </file> * 
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
#define DEBUG_SOCIAL


namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using LP = SocialPlatforms.LocalImpl;


    //--------------------------------------------------------------
    /// <summary> A high level social manager that does the
    ///           initializatio work for the SocailAPI. </summary>
    //--------------------------------------
    public class SocialManager : MonoBehaviour
    {
        public LP.SocialConfiguration localSocialData;



        //--------------------------------------------------------------
        /// <summary> Initializes the SocialManager </summary>
        //--------------------------------------
        void Start()
        {
            LP.SocialPlatform.Activate(localSocialData);
            Social.localUser.Authenticate(SocialAuthentication);
        }



        //--------------------------------------------------------------
        /// <summary> Reports authentication success </summary>
        /// <param name="success"></param>
        //--------------------------------------
        void SocialAuthentication(bool success)
        {
#if DEBUG_SOCIAL
            if (success)
            {
                Debug.Log("Social authentication successful");
            }
            else
            {
                Debug.Log("Social authentication failed");
            }
#endif
        }
    }
}

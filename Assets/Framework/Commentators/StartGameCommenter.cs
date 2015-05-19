/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                    StartGameCommenter.cs                    </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    26-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    public class StartGameCommenter : MonoBehaviour
    {
        public static bool announced = false;


        int frametimer;

        // Use this for initialization
        void Start()
        {
            frametimer = 5;
        }

        // Update is called once per frame
        void Update()
        {
            if (!announced)
            {
                frametimer--;

                if (frametimer <= 0)
                {
                    announced = Commentator.Comment(CommentatorEvent.GAME_START);
                }
            }
        }
    }
}
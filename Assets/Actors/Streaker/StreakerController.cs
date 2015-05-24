/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     StreakerController.cs                   </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    20-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    //
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-= 
    public partial class StreakerController : BaseCharacterController
    {
        partial void Setup_Dashing();
        partial void Setup_Throwing();
        partial void Process_Dashing();


        protected void Start()
        {
            Setup_Dashing();
            Setup_Throwing();
        }

        protected override void Update()
        {
            base.Update();
            Process_Dashing();
        }
    }
}
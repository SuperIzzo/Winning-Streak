/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     IThrowableObject.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    08-Oct-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using Characters;
    using UnityEngine;

    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> A common interface for throwable objects. </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public interface IThrowableObject
    {
        bool isThrown { get; }
        bool isHeld { get; }
        IThrowingCharacter thrower { get; }
        Vector3 throwForce { get; }

        void OnGrabbed( IThrowingCharacter character, Transform propSlot );
        void OnThrown( IThrowingCharacter character, Vector3 force );
    }
}
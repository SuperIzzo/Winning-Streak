/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      CensorBarLookAt.cs                     </file> * 
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
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;



    [AddComponentMenu("Miscellaneous/Billboard", 102)]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Rotates the transform towards a target </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class Billboard : MonoBehaviour
    {
        //..............................................................
        #region            //  INSPECTOR FIELDS  //
        //--------------------------------------------------------------
        /// <summary> The object this billboard rotates towards </summary>
        /// <remarks> Defaults to Camera.main </remarks>
        //--------------------------------------
        [Tooltip("The object this billboard rotates towards. \n" +
                 "Defaults to the main camera.")]
        [SerializeField]            Transform _lookAt           = null;
        #endregion
        //......................................



        //..............................................................
        #region              //  PROTECTED METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initializes the component </summary>
        //--------------------------------------
        protected void Start()
        {
            if(!_lookAt)
                _lookAt = Camera.main.transform;
        }



        //--------------------------------------------------------------
        /// <summary> Rotates the object towards the camera </summary>
        //--------------------------------------
        protected void FixedUpdate()
        {
            if( _lookAt )
            {
                transform.LookAt( _lookAt );
            }
        }


        #endregion
        //......................................
    }
}

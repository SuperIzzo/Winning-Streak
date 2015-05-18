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
namespace RoaringSnail
{
    using UnityEngine;



    [System.Flags]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Rotates the transform towards a target </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    internal enum AxesFlags
    {
        X = (1 << 0),
        Y = (1 << 1),
        Z = (1 << 2),
    }



    [AddComponentMenu("Miscellaneous/Billboard", 102)]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Rotates the transform towards a target </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class Billboard : MonoBehaviour
    {
        //--------------------------------------------------------------
        /// <summary> A bitmask containing all three axis flags </summary>
        //--------------------------------------
        static readonly AxesFlags ALL_AXES 
                                    = AxesFlags.X | AxesFlags.Y | AxesFlags.Z;



        //..............................................................
        #region            //  INSPECTOR FIELDS  //
        //--------------------------------------------------------------
        /// <summary> The object this billboard rotates towards </summary>
        /// <remarks> Defaults to Camera.main </remarks>
        //--------------------------------------
        [Tooltip("The object this billboard rotates towards. \n" +
                 "Defaults to the main camera.")]
        [SerializeField]            Transform _target           = null;


        //--------------------------------------------------------------
        /// <summary> Freeze rotation along axes </summary>
        //--------------------------------------
        [Tooltip("Freeze rotation along axes")]
        [SerializeField, BitMask]   AxesFlags _freezeRotation   = 0;
        #endregion
        //......................................



        //..............................................................
        #region              //  PROTECTED METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initializes the component </summary>
        //--------------------------------------
        protected void Start()
        {
            if(!_target)
                _target = Camera.main.transform;
        }



        //--------------------------------------------------------------
        /// <summary> Rotates the object towards the camera </summary>
        //--------------------------------------
        protected void Update()
        {
            if( _target )
            {
                // If there's no freezing we just look at target
                if (_freezeRotation == 0)
                {
                    transform.LookAt(_target);
                }
                // If all axes are frozen we skip
                else if( (_freezeRotation & ALL_AXES) == ALL_AXES)
                {
                    return;
                }
                // Otherwise we'll have to rotate partially 
                // (by reverting individual axes)
                else
                {
                    Vector3 oldRotation = transform.rotation.eulerAngles;

                    transform.LookAt(_target);

                    Vector3 newRotation = transform.rotation.eulerAngles;


                    if ((_freezeRotation & AxesFlags.X) > 0)
                        newRotation.x = oldRotation.x;

                    if ((_freezeRotation & AxesFlags.Y) > 0)
                        newRotation.y = oldRotation.y;

                    if ((_freezeRotation & AxesFlags.Z) > 0)
                        newRotation.z = oldRotation.z;

                    transform.rotation = Quaternion.Euler(newRotation);
                }
            }
        }
        #endregion
        //......................................
    }
}

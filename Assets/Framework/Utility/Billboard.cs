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
        [SerializeField]            Transform _lookAt           = null;


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
                // If there's no freezing we just look at target
                if (true || _freezeRotation == 0)
                {
                    transform.LookAt(_lookAt);
                }
                // else if not all axes are frozen
                // we rotate partially
                else if( !AreAllAxesFrozen() )
                {
                    Vector3 oldRotation = transform.rotation.eulerAngles;

                    transform.LookAt(_lookAt);

                    Vector3 newRotation = transform.rotation.eulerAngles;

                    // Eliminate individual axes
                    newRotation = FreezeIndividualAxes( _freezeRotation, 
                                                        oldRotation, 
                                                        newRotation  );
                    

                    transform.rotation = Quaternion.Euler(newRotation);
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Sellectively assigns axes from old to new </summary>
        //--------------------------------------
        private Vector3 FreezeIndividualAxes(   AxesFlags axes, 
                                                Vector3 oldRotation, 
                                                Vector3 newRotation )
        {
            if ((axes & AxesFlags.X) > 0)
                newRotation.x = oldRotation.x;

            if ((axes & AxesFlags.Y) > 0)
                newRotation.y = oldRotation.y;

            if ((axes & AxesFlags.Z) > 0)
                newRotation.z = oldRotation.z;

            return newRotation;
        }



        //--------------------------------------------------------------
        /// <summary> Returns whether rotation is frozen on all axes </summary>
        //--------------------------------------
        private bool AreAllAxesFrozen()
        {
            return (_freezeRotation & ALL_AXES) == ALL_AXES;
        }
        #endregion
        //......................................
    }
}

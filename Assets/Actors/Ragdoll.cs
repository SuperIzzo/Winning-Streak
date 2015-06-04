/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          Ragdoll.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    01-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using System.Collections.Generic;

    //--------------------------------------------------------------
    /// <summary> Ragdoll character "animator". </summary>
    /// <description> Puts a rigged character into a ragdoll mode
    /// disabling the character animator, and back.
    /// </description>
    //--------------------------------------
    [AddComponentMenu("Physics/Ragdoll")]
    public class Ragdoll : MonoBehaviour
    {
        //--------------------------------------------------------------
        #region Public settings
        //--------------------------------------
        /// <summary> Reference to the the animator component. </summary>
        public Animator animator;

        /// <summary> Indicates whether the parts list 
        /// should be generated authomatically. </summary>
        /// <description> 
        /// If <c>automaticPartsList<c> is <c>true<c>
        /// the <c>parts<c> list will be generated on the
        /// fly from child <see cref="Rigidbody"/>-s
        /// </description>
        public bool automaticPartsList = true;

        /// <summary> The list of <see cref="Rigidbody"/> 
        /// ragdoll parts. </summary>
        public List<Rigidbody> parts;
        #endregion


        //--------------------------------------------------------------
        #region Internal state
        //--------------------------------------
        /// <summary> Whether the ragdoll is active. </summary>
        private bool _activated = false;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this 
        /// <see cref="Ragdoll"/> is activated. </summary>
        /// <value><c>true</c> if activated; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public bool activated
        {
            get { return _activated; }
            set
            {
                if (_activated != value)
                {
                    _activated = value;
                    if (_activated)
                        ActivateRagdoll();
                    else
                        DeactivateRagdoll();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Initiates the ragdoll. </summary>
        //----------------------------------
        void Start()
        {
            if (_activated)
                ActivateRagdoll();
            else
                DeactivateRagdoll();
        }



        //--------------------------------------------------------------
        /// <summary> Activates the ragdoll and disables other 
        /// character animations. </summary>
        //--------------------------------------
        void ActivateRagdoll()
        {
            if (animator)
                animator.enabled = false;

            if (automaticPartsList)
                UpdateParts();

            foreach (Rigidbody part in parts)
            {
                if (part)
                {
                    part.isKinematic = false;

                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Deactivates the ragdoll and re-enables other 
        /// character animations. </summary>
        //--------------------------------------
        void DeactivateRagdoll()
        {
            if (animator)
                animator.enabled = true;

            if (automaticPartsList)
                UpdateParts();

            foreach (Rigidbody part in parts)
            {
                if (part)
                {
                    part.isKinematic = true;
                    part.freezeRotation = false;
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Automatically updates the ragdoll parts list </summary>
        /// <description> Builds up a new list of all <see cref="Rigidbody"/>-s
        /// in children of this GameObject. </description>
        //--------------------------------------
        void UpdateParts()
        {
            parts = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        }
    }
}
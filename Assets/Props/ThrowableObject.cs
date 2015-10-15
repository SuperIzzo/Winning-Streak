/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                      ThrowableObject.cs                     </file> * 
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
    using System;
    using Characters;
    using UnityEngine;


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> A generic throwable object. </summary>
    // TODO: Inerit from Grabbable and move logic common 
    // to both throwable and usable to there
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class ThrowableObject : MonoBehaviour, IThrowableObject
    {
        //..............................................................
        #region                  //  FIELDS  //
        //--------------------------------------------------------------
        [SerializeField, Tooltip
        (   "The force threshold required to knock out a character."  )]
        //--------------------------------------
        float knockOutPower = 5.0f;
        #endregion
        //......................................



        //..............................................................
        #region              //  PUBLIC PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Gets or sets a value indicating whether this
        /// <see cref="BaseCharacterController" /> is dashing. </summary>
        /// <value>
        ///   <c>true</c> if dashing; otherwise, <c>false</c>.</value>
        //--------------------------------------
        public IThrowingCharacter thrower { get; private set; }
        public Vector3 throwForce { get; private set; }
        public bool isThrown { get { return throwForce.magnitude > 0; } }
        public bool isHeld { get; private set; }
        #endregion



        //--------------------------------------------------------------
        #region Priavate references
        //--------------------------------------
        private Transform slot;
        private Transform _oldParent;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Update is called once per frame </summary>
        //--------------------------------------
        protected virtual void Update()
        {
            // Fly to the slot holder's hand when held
            if( slot && transform.parent )
            {
                transform.localPosition = Vector3.Lerp( transform.localPosition, Vector3.zero, 0.2f );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Called when the throwable is grabbed. </summary>
        /// <param name="character">The grabbing character.</param>
        /// <param name="propSlot">The slot for the prop.</param>
        //--------------------------------------
        public virtual void OnGrabbed( IThrowingCharacter character, Transform propSlot )
        {
            thrower = character;
            slot = propSlot;

            if( GetComponent<Rigidbody>() )
                GetComponent<Rigidbody>().isKinematic = true;

            if( GetComponent<Collider>() )
                GetComponent<Collider>().isTrigger = true;

            // Get addopted by the hand/slot
            // We keep the world position, as we'll animate the grabbing
            _oldParent = transform.parent;
            transform.SetParent( slot, true );
            isHeld = true;
        }



        //--------------------------------------------------------------
        /// <summary> Called when the prop is thrown. </summary>
        /// <param name="character">The throwing character.</param>
        /// <param name="force">The force at which the object is thrown.</param>
        //--------------------------------------
        public virtual void OnThrown( IThrowingCharacter character, Vector3 force )
        {
            if( GetComponent<Collider>() )
                GetComponent<Collider>().isTrigger = false;

            if( GetComponent<Rigidbody>() )
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce( force, ForceMode.Impulse );
            }


            // Unlink, note we keep it in world space
            transform.SetParent( _oldParent, true );
            slot = null;
            throwForce = force;
            isHeld = false;
        }



        //--------------------------------------------------------------
        /// <summary> Raises the collision enter event. </summary>
        /// <param name="collision">Collision data.</param>
        //--------------------------------------
        void OnCollisionEnter( Collision collision )
        {
            if( isThrown )
            {
                var character = collision.collider.GetComponentInChildren<BaseCharacterController>();

                if( character )
                {
                    if( character != (object) thrower )
                    {
                        throwForce = Vector3.zero;
                        thrower = null;

                        if( collision.relativeVelocity.magnitude > knockOutPower
                               && GetComponent<Rigidbody>().velocity.magnitude > knockOutPower )
                        {
                            character.KnockDown();
                        }
                    }
                }
                else
                {
                    thrower = null;
                }
            }
        }

    }
}
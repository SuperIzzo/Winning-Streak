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
    using Characters;
    using UnityEngine;


    //--------------------------------------------------------------
    /// <summary> A base class for all throwable objects. </summary>
    // TODO: Inerit from Grabbable and move logic common to both throwable and usable to there
    //--------------------------------------
    public class ThrowableObject : MonoBehaviour
    {
        //--------------------------------------------------------------
        #region Public settings
        //--------------------------------------
        public float knockOutPower = 5.0f;
        public BaseCharacterController owner { get; private set; }
        #endregion



        //--------------------------------------------------------------
        #region Public properties
        //--------------------------------------
        public Vector3 throwForce { get; private set; }
        public bool isThrown { get { return throwForce.magnitude > 0; } }
        #endregion



        //--------------------------------------------------------------
        #region Priavate references
        //--------------------------------------
        private Transform slot;
        #endregion



        //--------------------------------------------------------------
        /// <summary> Update is called once per frame </summary>
        //--------------------------------------
        void Update()
        {
            // Fly to the slot holder's hand when held
            if (slot && transform.parent)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.2f);
            }
        }



        //--------------------------------------------------------------
        /// <summary> Called when the throwable is grabbed. </summary>
        /// <param name="character">The grabbing character.</param>
        /// <param name="propSlot">The slot for the prop.</param>
        //--------------------------------------
        public void OnGrabbed(BaseCharacterController character, Transform propSlot)
        {
            owner = character;
            slot = propSlot;

            if (GetComponent<Rigidbody>())
                GetComponent<Rigidbody>().isKinematic = true;

            if (GetComponent<Collider>())
                GetComponent<Collider>().isTrigger = true;

            // Get addopted by the hand/slot
            // We keep the world position, as we'll animate the grabbing
            transform.SetParent(slot, true);            
        }



        //--------------------------------------------------------------
        /// <summary> Called when the prop is thrown. </summary>
        /// <param name="character">The throwing character.</param>
        /// <param name="force">The force at which the object is thrown.</param>
        //--------------------------------------
        public void OnThrown(BaseCharacterController character, Vector3 force)
        {
            if (GetComponent<Collider>())
                GetComponent<Collider>().isTrigger = false;

            if (GetComponent<Rigidbody>())
            {
                GetComponent<Rigidbody>().isKinematic = false;
                GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            }


            // Unlink, note we keep it in world space
            transform.SetParent(null, true);
            slot = null;
            throwForce = force;
        }



        //--------------------------------------------------------------
        /// <summary> Raises the collision enter event. </summary>
        /// <param name="collision">Collision data.</param>
        //--------------------------------------
        void OnCollisionEnter(Collision collision)
        {
            if (isThrown)
            {
                var character = collision.collider.GetComponentInChildren<BaseCharacterController>();

                if (character)
                {
                    if (character != owner)
                    {
                        throwForce = Vector3.zero;
                        owner = null;

                        if (collision.relativeVelocity.magnitude > knockOutPower
                               && GetComponent<Rigidbody>().velocity.magnitude > knockOutPower)
                        {
                            character.KnockDown();
                        }
                    }
                }
                else
                {
                    owner = null;
                }
            }
        }

    }
}
/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                    DamageableCharacter.cs                   </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    13-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using Characters;
    using System.Collections;

    //--------------------------------------------------------------
    /// <summary> Makes a GameObject with a <see cref="BaseCharacterController"/> 
    /// component damageable. </summary>
    //--------------------------------------
    [AddComponentMenu("Damage/DamageableCharacter", 1)]
    [RequireComponent(typeof(BaseCharacterController))]
    public class DamageableCharacter : Damageable
    {
        public Rigidbody rootBone;
        public AudioClip tackleSFX;
        private BaseCharacterController controller;

        //--------------------------------------------------------------
        /// <summary> Unity Start callback. </summary>
        /// <description> Initializes components. </description>
        //--------------------------------------
        void Start()
        {
            controller = GetComponent<BaseCharacterController>();
        }

        //--------------------------------------------------------------
        /// <summary> Callback for a character taking damage. </summary>
        /// <description> Knocks down the character. </description>
        //--------------------------------------
        public override void OnDamage(DamageInfo info)
        {
            if (controller)
                controller.isKnockedDown = true;


            // HACK: Don't ask
            // This applies raw force to damagees based on the movement speed
            // of the tackling character
            if (rootBone)
            {
                var damagerController = info.damager.GetComponentInParent<BaseCharacterController>();
                if (damagerController)
                {
                    // Play sound effect
                    if (GetComponent<AudioSource>() && tackleSFX)
                    {
                        GetComponent<AudioSource>().PlayOneShot(tackleSFX);
                    }

                    Vector3 direction = transform.position - info.damager.transform.position;
                    direction += damagerController.lookDirection * 3;

                    //if( info.damager.rigidbody )
                    //	info.damager.rigidbody.velocity *= 0.3f;

                    // Eliminate Y (we'll add it artificially later)
                    direction.y = 0;

                    float yFactor = 0.07f + Random.value * 0.04f;
                    direction.y = direction.magnitude * yFactor;
                    direction.Normalize();

                    float forceMagnitude = 80.0f + (Random.value * Random.value * 60.0f);

                    Vector3 force = direction * forceMagnitude;


                    StartCoroutine(ApplyForce(force));
                }
            }
        }


        private IEnumerator ApplyForce(Vector3 force)
        {
            yield return null;

            rootBone.isKinematic = false;
            rootBone.AddForce(force, ForceMode.Impulse);
        }
    }
}
/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     CharacterAnimator.cs                    </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    28-Nov-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    using UnityEngine;


    //--------------------------------------------------------------
    /// <summary> Character animator. </summary>
    /// <description> 
    /// Animates the character 3D model based on the character 
    /// controller.
    /// </description>
    //--------------------------------------
    [AddComponentMenu("Winning Streak/Character/Character Animator", 200)]
    public class CharacterAnimator : MonoBehaviour
    {
        //--------------------------------------------------------------
        #region Public settings
        //--------------------------------------
        /// <summary> The goofiness paramater for the animation. </summary>
        [Range(0.0f, 1.0f)]
        public float goofiness;
        #endregion


        //--------------------------------------------------------------
        #region Public references
        //--------------------------------------
        /// <summary> The character controller component. </summary>
        public GameObject character;

        /// <summary> The animator component. </summary>
        public Animator animator;

        /// <summary> The ragdoll component. </summary>
        public Ragdoll ragdoll;

        public Rigidbody rootBone;
        #endregion


        private IKnockableCharacter _knockable;
        private IMobileCharacter _mover;
        private IDancingCharacter _dancer;
        private IThrowingCharacter _thrower;
        private ITacklingCharacter _tackler;


        void Start()
        {
            if( character )
            {
                _knockable  = character.GetComponent<IKnockableCharacter>();
                _mover = character.GetComponent<IMobileCharacter>();
                _dancer = character.GetComponent<IDancingCharacter>();
                _thrower = character.GetComponent<IThrowingCharacter>();
                _tackler = character.GetComponent<ITacklingCharacter>();
            }
        }

        //--------------------------------------------------------------
        /// <summary> Update is called once per frame </summary>
        //--------------------------------------
        void Update()
        {
            // Only update the animation if time is running
            if (Mathf.Abs(Time.deltaTime) <= float.Epsilon)
                return;


            float speed = 0;
            if (_mover != null)
            { 
                speed =  _mover.movementSpeed / 8.0f;
                speed *= _mover.relativeVelocity.magnitude;
            }
            
            bool isDancing =     (_dancer != null) && _dancer.isDancing;
            bool isTackling =    (_tackler != null) && _tackler.isTackling;
            bool isCharging =    (_thrower != null) && _thrower.isCharging;
            bool isKnockedDown = (_knockable != null) && _knockable.isKnockedDown;


            animator.SetFloat("speed", speed);
            animator.SetFloat("goofiness", goofiness);
            animator.SetBool("wiggle", isDancing);
            animator.SetBool("tackle", isTackling);
            animator.SetBool("charge_throw", isCharging);

            // Fix the character position based on the ragdoll simulations
            if (ragdoll.activated && !isKnockedDown && rootBone)
            {
                Vector3 position = rootBone.transform.position;
                position.y = character.transform.position.y;
                character.transform.position = position;
            }

            // ragdoll activates when the character is knocked down
            ragdoll.activated = isKnockedDown;
        }
    }
}
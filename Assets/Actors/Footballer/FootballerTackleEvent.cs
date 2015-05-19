/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   FootballerTackleEvent.cs                  </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    11-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;



    [AddComponentMenu("Winning Streak/Character/Footballer Tackle Event", 102)]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Handles special effects and scoring,
    ///           when the footballer tackles            </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class FootballerTackleEvent : MonoBehaviour
    {
        //..............................................................
        #region            //  INSPECTOR FIELDS  //
        //--------------------------------------------------------------
        [SerializeField] BaseCharacterController    _controller = null;
        [SerializeField] AIInput                    _aiInput    = null;

        [SerializeField] float                      _tackleMissTime   = 5.0f;
        [SerializeField] AudioClip[]                _tackleFailSFX    = null;
        [SerializeField] AudioClip[]                _tackleSucceedSFX = null;
        #endregion
        //......................................


        //..............................................................
        #region             //  PRIVATE FIELDS  //
        //--------------------------------------------------------------
        private BaseCharacterController             _player = null;
        private AudioSource                         _audio  = null;
        private float                               _tackleMissTimer  = 0.0f;
        #endregion
        //......................................


        //..............................................................
        #region                 //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initializes the component </summary>
        //--------------------------------------
        protected void Start()
        {
            _player     = Player.p1.characterController;
            _audio      = GetComponent<AudioSource>();
            _controller = GetComponent<BaseCharacterController>();
            _aiInput    = GetComponent<AIInput>();
        }



        //--------------------------------------------------------------
        /// <summary> Checks for tackle completion </summary>
        //--------------------------------------
        protected void Update()
        {
            // HACK:    FootballerTackleEvent shouldn't be guessing
            //          when the tackle has to happen, this should be an event
            if (_controller.isTackling && _tackleMissTimer <= 0)
            {
                _tackleMissTimer = _tackleMissTime;
            }

            if (_tackleMissTimer > 0)
            {
                _tackleMissTimer -= Time.deltaTime;

                if (_tackleMissTimer <= 0)
                {
                    ResolveTackle();
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Handles the tackle event </summary>
        //--------------------------------------
        private void ResolveTackle()
        {
            if (!_player.isKnockedDown && _aiInput.target == _player)
            {
                // The player is alive... the tackle must have failed
                var scoringEvent = GameSystem.scoringEvent;

                scoringEvent.Fire( ScoringEventType.DODGE_TACKLE );
                PlayRandomClip( _tackleFailSFX );
            }
            else
            {
                // The player is dead... and we were targeting him
                // the tackle must have succeeded
                PlayRandomClip( _tackleSucceedSFX );
            }
        }



        //--------------------------------------------------------------
        /// <summary> Plays a random clip from a list of clips </summary>
        //--------------------------------------
        private void PlayRandomClip( AudioClip[] clips )
        {
            if( _audio && clips.Length>0 )
            {
                int clipIdx = Random.Range(0, clips.Length);
                _audio.clip = clips[clipIdx];
                _audio.Play();
            }
        }
        #endregion
        //......................................
    }
}
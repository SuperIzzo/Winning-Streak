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
namespace RoaringSnail.WinningStreak.Characters
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
        [SerializeField] AudioClip[]                _tackleFailSFX    = null;
        [SerializeField] AudioClip[]                _tackleSucceedSFX = null;
        [SerializeField] GameObject                 _dustEffect       = null;
        [SerializeField] Transform                  _dustPosition     = null;
        #endregion
        //......................................


        //..............................................................
        #region             //  PRIVATE FIELDS  //
        //--------------------------------------------------------------
        private BaseCharacterController             _player  = null;
        private AudioSource                         _audio   = null;
        private ITacklingCharacter                  _tackler = null;
        private AIInput                             _aiInput = null;
        private bool                                _playFailSfx = false;
        #endregion
        //......................................



        private BaseCharacterController player
        {
            get
            {
                if( _player == null )
                    _player = Player.p1.characterController;

                return _player;
            }

            set
            {
                _player = value;
            }
        }


        //..............................................................
        #region                 //  METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initializes the component </summary>
        //--------------------------------------
        protected void Awake()
        {            
            _audio      = GetComponent<AudioSource>();
            _tackler    = GetComponent<ITacklingCharacter>();
            _aiInput    = GetComponent<AIInput>();

            if( _tackler!=null )
            {
                _tackler.Tackled += OnTackled;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Checks for tackle completion </summary>
        //--------------------------------------
        protected void OnTackled(object s, System.EventArgs e )
        {
            ResolveTackle();
        }



        //--------------------------------------------------------------
        /// <summary> Handles the tackle event </summary>
        //--------------------------------------
        private void ResolveTackle()
        {
            if( player != null )
            {
                if( !player.isKnockedDown )
                {
                    // The player is alive... the tackle must have failed
                    var scoringEvent = GameSystem.scoringEvent;

                    scoringEvent.Fire( ScoringEventType.DODGE_TACKLE );
                    _playFailSfx = true;                                        
                }
                else
                {
                    // The player is dead... and we were targeting him
                    // the tackle must have succeeded
                    PlayRandomClip( _tackleSucceedSFX );
                }
            }
        }



        //--------------------------------------------------------------
        /// <summary> Plays fail sfx on contact with the ground </summary>
        //--------------------------------------
        protected void OnCollisionEnter(Collision col)
        {
            if( _playFailSfx && col.collider.CompareTag(Tags.field) )
            {
                PlayRandomClip( _tackleFailSFX );                

                if( _dustEffect )
                {
                    Vector3 pos = transform.position;

                    if( _dustPosition )
                        pos = _dustPosition.position;

                    Instantiate(  _dustEffect,
                                  pos,
                                  Quaternion.identity );
                }
                _playFailSfx = false;
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
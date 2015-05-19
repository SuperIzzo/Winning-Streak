/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   RandomCharacterShouts.cs                  </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using System.Collections;
    


    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("Audio/Random Audio Player", 101)]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Plays random audio clips </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class RandomAudioClipPlayer : MonoBehaviour
    {
        [SerializeField] AudioClip[]    _audioClips = null;
        [SerializeField] MinMax         _timeSpan;

        private AudioSource             _audio;


        //--------------------------------------------------------------
        /// <summary> Initializes the audio player </summary>
        //--------------------------------------
        protected void Start()
        {
            StartCoroutine(PlayAudio());
        }



        //--------------------------------------------------------------
        /// <summary> Refreshes external references </summary>
        //--------------------------------------
        protected void OnEnable()
        {
            _audio = GetComponent<AudioSource>();
        }



        //--------------------------------------------------------------
        /// <summary> Plays random audio clips at random intervals </summary>
        //--------------------------------------
        IEnumerator PlayAudio()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeSpan.Random());

                // TODO: Play sounds only of the character's not dead
                if( enabled && _audio &&
                    _audioClips!=null && 
                    _audioClips.Length>0 )
                {
                    _audio.clip = _audioClips[Random.Range(0, _audioClips.Length)];
                    _audio.Play();
                }
            }
        }
    }
}

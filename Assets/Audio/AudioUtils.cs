/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        AudioUtils.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    08-Apr-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using UnityEngine.Audio;

    //--------------------------------------------------------------
    /// <summary> Audio utilities. </summary>
    //-------------------------------------- 
    public static class AudioUtils
    {
        //--------------------------------------------------------------
        /// <summary> Component list for oneshot sounds. </summary>
        //--------------------------------------
        private static readonly System.Type[] ONE_SHOT_SOUND_COMPONENTS =
        {
        typeof(AudioSource),
        typeof(DestroyOnAudioSourceCompletion)
    };

        //--------------------------------------------------------------
        /// <summary> Plays one shot sound effect. </summary>
        /// <returns> The constructed game object.</returns>
        /// <param name="parent">Parent</param>
        /// <param name="clip">Clip</param>
        /// <param name="pan">Pan</param>
        /// <param name="vol">Volume</param>
        //--------------------------------------
        public static GameObject PlayOneShot(Component parent, AudioClip clip, float pan, float vol)
        {
            AudioMixerGroup parentMixerGroup = null;
            GameObject go = new GameObject("SoundEffect", ONE_SHOT_SOUND_COMPONENTS);

            if (parent)
            {
                // Group all created objects under the parent
                go.transform.parent = parent.transform;

                AudioSource parentSource = parent.GetComponent<AudioSource>();
                if (parentSource)
                {
                    parentMixerGroup = parentSource.outputAudioMixerGroup;
                }
            }

            AudioSource audioSource = go.GetComponent<AudioSource>();
            audioSource.volume = vol;
            audioSource.panStereo = pan;
            audioSource.clip = clip;
            audioSource.outputAudioMixerGroup = parentMixerGroup;

            audioSource.Play();

            return go;
        }
    }
}
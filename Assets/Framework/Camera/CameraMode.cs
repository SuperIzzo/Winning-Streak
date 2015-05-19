/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        CameraMode.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    03-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    public class CameraMode : MonoBehaviour
    {
        public MonoBehaviour[] modes;

        private int currentMode = 0;

        // Use this for initialization
        void Start()
        {
            UpdateModes();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("CameraMode"))
            {
                NextMode();
            }
        }

        void NextMode()
        {
            do
            {
                currentMode++;
                currentMode %= modes.Length;
            }
            while (modes[currentMode] == null);

            UpdateModes();
        }

        void UpdateModes()
        {
            foreach (MonoBehaviour mode in modes)
            {
                if (mode != modes[currentMode])
                    mode.enabled = false;
            }

            modes[currentMode].enabled = true;
        }
    }
}
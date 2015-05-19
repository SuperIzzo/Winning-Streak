/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         GUIWindow.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    03-Dec-2014                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class GUIWindow : MonoBehaviour
    {
        public Animator windowAnimator;
        public GameObject selectedObject;
        private bool _isVisible;
        public bool isVisible
        {
            set { if (value) Show(); else Hide(); }
            get { return _isVisible; }
        }

        // Use this for initialization
        void Start()
        {
            if (!windowAnimator)
            {
                windowAnimator = GetComponent<Animator>();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show()
        {
            if (!_isVisible)
            {
                _isVisible = true;

                if (windowAnimator)
                    windowAnimator.SetBool("visible", true);
            }
        }

        public void Hide()
        {
            if (_isVisible)
            {
                _isVisible = false;

                if (windowAnimator)
                    windowAnimator.SetBool("visible", false);
            }
        }

        public virtual void OnShow()
        {
            GainInputFocus();
        }

        private void GainInputFocus()
        {
            if (selectedObject)
                EventSystem.current.SetSelectedGameObject(selectedObject);
        }
    }
}
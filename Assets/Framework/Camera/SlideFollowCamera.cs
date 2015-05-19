/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     SlideFollowCamera.cs                    </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    08-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    //--------------------------------------------------------------
    /// <summary> Camera configuratin for 
    /// following behaviour. </summary>
    //--------------------------------------
    [System.Serializable]
    public class FollowCameraConfig
    {
        public Vector3 lookAtOffset = new Vector3();
        public Vector3 positionOffset = new Vector3();
        public float followSpeed = 6;
        public float turnSpeed = 1;
        public float FOVModifier = 1;
    }

    //--------------------------------------------------------------
    /// <summary>  Slide follow camera behaviour. </summary>
    /// <description>
    /// SlideFollowCamera imitates the behaviour of stadium cameras 
    /// which slide on a wire. The camera has four degrees of
    /// freedom:
    /// <list type="bullet">
    /// 	<item> Movement along the global x axis 
    /// 		   (along the lenght of the stadium).</item>
    /// 	<item> Zoom forward </item>
    /// 	<item> Rotation along the x and y local axes </item> 
    /// </list>
    /// 
    /// In addition the camera can have multiple parameter configs.
    /// </description>
    //--------------------------------------
    public class SlideFollowCamera : MonoBehaviour
    {
        public FollowCameraConfig normal;
        public FollowCameraConfig zoomed;

        private Transform target;
        private FollowCameraConfig currentConfig;

        //--------------------------------------------------------------
        /// <summary> Zooms the camera in on the target. </summary>
        //--------------------------------------
        public void ZoomIn()
        {
            currentConfig = zoomed;
        }

        //--------------------------------------------------------------
        /// <summary> Resets the zoom configuration. </summary>
        //--------------------------------------
        public void ZoomReset()
        {
            currentConfig = normal;
        }

        //--------------------------------------------------------------
        /// <summary> Start callback. </summary>
        //--------------------------------------
        void Start()
        {
            currentConfig = normal;
        }

        //--------------------------------------------------------------
        /// <summary> Update callback. </summary>
        //--------------------------------------
        void Update()
        {
            if (target == null)
            {
                target = Player.p1.transform;
            }

            if (target != null)
            {
                PickZoomConfiguration();

                LookAtTarget();
                TrackTarget();
                ZoomToTarget();
            }
        }

        //--------------------------------------------------------------
        /// <summary> Looks at the target target. </summary>
        //--------------------------------------
        private void LookAtTarget()
        {
            Vector3 directionToTarget = target.position - transform.position;
            Quaternion targetRot = Quaternion.LookRotation(directionToTarget);

            float deltaTurn = Time.unscaledDeltaTime * currentConfig.turnSpeed;
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, deltaTurn);
        }

        //--------------------------------------------------------------
        /// <summary> Slide along the wire towards the target. </summary>
        //--------------------------------------
        private void TrackTarget()
        {
            //side to side tracking
            Vector3 newPos = new Vector3(
                target.position.x,
                currentConfig.positionOffset.y,
                currentConfig.positionOffset.z);

            float deltaMove = Time.unscaledDeltaTime * currentConfig.followSpeed;
            transform.position = Vector3.Lerp(transform.position, newPos, deltaMove);
        }

        //--------------------------------------------------------------
        /// <summary> Looks at target. </summary>
        //--------------------------------------
        private void ZoomToTarget()
        {
            //fov tracking
            Vector3 invertedCamera = new Vector3(
                transform.position.x,
                transform.position.y,
                  -transform.position.z - 10);

            float fieldOfView = Vector3.Distance(target.position, invertedCamera);
            fieldOfView *= currentConfig.FOVModifier;
            GetComponent<Camera>().fieldOfView = fieldOfView;
        }

        //--------------------------------------------------------------
        /// <summary> Choses a zoom configuration </summary>
        //--------------------------------------
        private void PickZoomConfiguration()
        {
            TimeFlow timeFlow = GameSystem.timeFlow;

            if (timeFlow.isSlowed)
                ZoomIn();
            else
                ZoomReset();
        }
    }
}
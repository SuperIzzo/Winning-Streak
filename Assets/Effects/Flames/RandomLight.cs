/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        RandomLight.cs                       </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    06-Oct-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;


    public class RandomLight : MonoBehaviour
    {
        [SerializeField]
        float _intensityMin;

        [SerializeField]
        float _intensityMax;

        [SerializeField]
        float _intensitySmoothness;

        Light _light;

        protected void Awake()
        {
            _light = GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            float newIntensity = Random.Range( _intensityMin, _intensityMax );
            _light.intensity = 
                _light.intensity * _intensitySmoothness
                + newIntensity * (1 - _intensitySmoothness);
        }
    }
}
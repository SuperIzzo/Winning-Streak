/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                           MinMax.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    18-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail
{
    using System;
    using UnityEngine;



    [Serializable]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> A range min max pair  </summary>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public struct MinMax
    {
        [SerializeField] float _min;
        [SerializeField] float _max;



        //--------------------------------------------------------------
        /// <summary> Gets or sets the low value of the range </summary>
        /// <remarks> 
        ///     This property's value cannot exceed <c>max</c>, 
        ///     if assigned a value higher than <c>max</c> the value of 
        ///     <c>max</c> is assigned instead.
        /// </remarks>
        /// <value> the range min </value>
        //--------------------------------------
        public float min
        {
            get { return _min; }
            set { _min = Mathf.Min(_max, value); }
        }



        //--------------------------------------------------------------
        /// <summary> Gets or sets the high value of the range </summary>
        /// <remarks> 
        ///     This property's value cannot be lower than <c>min</c>, 
        ///     if assigned a value lower than <c>min</c> the value of 
        ///     <c>min</c> is assigned instead.
        /// </remarks>
        /// <value> the range max </value>
        //--------------------------------------
        public float max
        {
            get { return _max; }
            set { _max = Mathf.Max(_min, value); }
        }



        //--------------------------------------------------------------
        /// <summary> Creates a new range with the specified bounds </summary>
        /// <remarks> 
        ///     <c>a</c> and <c>b</c> are appropriately assigned to 
        ///     <c>min</c> and <c>max</c>. 
        /// </remarks>
        /// <param name="a"> The first boundary value </param>
        /// <param name="b"> The second boundary value </param>
        //--------------------------------------
        public MinMax(float a, float b)
        {
            _min = Mathf.Min(a, b);
            _max = Mathf.Max(a, b);
        }



        //--------------------------------------------------------------
        /// <summary> Retursn a random value from this range </summary>
        /// <returns> a random value from this range </returns>
        //--------------------------------------
        public float Random()
        {
            return UnityEngine.Random.Range(min,max);
        }



        //--------------------------------------------------------------
        /// <summary> Clamps a value within this range </summary>
        /// <param name="value"> the value to be clampped </param>
        /// <returns> 
        ///     <c>value</c> clamped between <c>min</c> and <c>max</c> 
        /// </returns>
        //--------------------------------------
        public float Clamp(float value)
        {
            return Mathf.Clamp(value, min, max);
        }
    }
}

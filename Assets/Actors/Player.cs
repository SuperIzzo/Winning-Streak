/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          Player.cs                          </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    27-Jan-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak
{
    using UnityEngine;
    using Characters;


    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary>   An static utility class that provides easy 
    ///             access to player related objects and components. 
    ///                                                   </summary>
    //   NOTE: In multiplayer this will be indexable 
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class Player
    {
        private static Player _p1;



        //..............................................................
        #region            //  PRIVATE REFERENCES  //
        //--------------------------------------------------------------
        private GameObject  _gameObject;
        private Score       _score;
        #endregion
        //......................................



        //..............................................................
        #region            //  PUBLIC PROPERTIES  //
        //--------------------------------------------------------------
        /// <summary> Returns player one. </summary>
        //--------------------------------------
        public static Player p1
        {
            get
            {
                if( _p1 == null )
                    _p1 = new Player();

                return _p1;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the player gameObject (readonly). </summary>
        /// <value> The player game object.</value>
        //--------------------------------------
        public GameObject gameObject
        {
            get
            {
                if( !_gameObject )
                    _gameObject = GameObject.FindGameObjectWithTag( Tags.player );

                return _gameObject;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the player transform (readonly). </summary>
        /// <value> The player transform.</value>
        //--------------------------------------
        public Transform transform
        {
            get
            {
                if( gameObject )
                    return gameObject.transform;
                else
                    return null;
            }
        }


        //--------------------------------------------------------------
        /// <summary> Returns the player character controller (readonly). </summary>
        /// <value> The player character controller.</value>
        //--------------------------------------
        public BaseCharacterController characterController
        {
            get
            {
                if( gameObject )
                    return gameObject.GetComponent<BaseCharacterController>();
                else
                    return null;
            }
        }



        //--------------------------------------------------------------
        /// <summary> Returns the player's score (readonly). </summary>
        /// <value> The player character controller.</value>
        //--------------------------------------
        public Score score
        {
            get
            {
                //HACK: There's only one player atm, so only one score
                //      this will change
                if( !_score )
                    _score = GameObject.FindObjectOfType<Score>();

                return _score;
            }
        }
        #endregion
        //......................................
    }
}
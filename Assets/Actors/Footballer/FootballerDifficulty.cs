/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   FootballerDifficulty.cs                   </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    16-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
namespace RoaringSnail.WinningStreak.Characters
{
    using UnityEngine;



    [AddComponentMenu( "Winning Streak/Character/Footballer Difficulty", 101 )]
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    /// <summary> Controls the footballers' stats based on difficulty.
    ///                                                    </summary>
    /// <remarks>
    ///     Footballer Difficulty increases the difficulty of 
    ///     footballers in respect with the global game difficulty.
    /// </remarks>
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
    public class FootballerDifficulty : MonoBehaviour
    {
        //..............................................................
        #region             //  PRIVATE FIELDS  //
        //--------------------------------------------------------------
        private AIInput                     _aiInput;
        private BaseCharacterController     _controller;
        private float                       _baseMovementSpeed  = 0.0f;
        private float                       _basePlayerHate     = 0.0f;
        #endregion
        //......................................



        //..............................................................
        #region              //  PROTECTED METHODS  //
        //--------------------------------------------------------------
        /// <summary> Initializes the component </summary>
        //--------------------------------------
        protected void Start()
        {
            _aiInput    = GetComponent<AIInput>();
            _controller = GetComponent<BaseCharacterController>();

            if( _controller )
                _baseMovementSpeed = _controller.baseMovementSpeed;

            if( _aiInput )
                _basePlayerHate = _aiInput.playerHate;
        }



        //--------------------------------------------------------------
        /// <summary> Updates footballer stats by difficulty </summary>
        //--------------------------------------
        protected void Update()
        {
            if( GameSystem.difficulty != null )
            {
                float difficultyLevel = GameSystem.difficulty.level;

                if( _aiInput )
                {
                    _aiInput.playerHate =
                        _basePlayerHate + (1 - _basePlayerHate) * difficultyLevel;
                }

                if( _controller )
                {
                    const int DIFFICULTY_MULT = 2;
                    _controller.baseMovementSpeed =
                        _baseMovementSpeed + difficultyLevel * DIFFICULTY_MULT;
                }
            }
        }
        #endregion
        //......................................
    }
}

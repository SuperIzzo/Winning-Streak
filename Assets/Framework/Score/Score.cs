/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                           Score.cs                          </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    04-Apr-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--------------------------------------------------------------
/// <summary> Score deals with scores and stuff. </summary>
//--------------------------------------
public class Score : MonoBehaviour
{
	//--------------------------------------------------------------
	#region		Private constants
	//--------------------------------------
	private static readonly float COMBO_TIME_OUT = 3.0f;
	private static readonly float MAX_COMBO_POINTS = 10.0f;
	private static readonly float COMBO_EFFECT = 1.6f;
    #endregion


    //--------------------------------------------------------------
    /// <summary> Dispatched when a score parameter changes. </summary>
    //--------------------------------------
    public class ScoreEventArgs : System.EventArgs
    {
        public Score score { get; private set; }
        public float points { get; private set; }

        internal ScoreEventArgs(Score score, float points)
        {
            this.score = score;
            this.points = points;
        }
    }


	//--------------------------------------------------------------
	#region		Public events
	//--------------------------------------
	public delegate void ScoreCallback(object sender, ScoreEventArgs e);
	public event ScoreCallback OnComboCompleted;
	#endregion


	//--------------------------------------------------------------
	#region		Public properties
	//--------------------------------------
	/// <summary> Gets or sets the base score. </summary>
	/// <description>
	/// 	The base score value can only be increased. The 
	/// 	recommended way to use this is with the += operator.
	/// </description>
	/// <value>The base score.</value>
	public float baseScore
	{
		get { return _baseScore; }

		set{ if(enabled) _baseScore = VerifySameOrBigger(_baseScore, value); }
	}

	//--------------------------------------
	/// <summary>	Gets or sets the combo builder 
	/// 		and resets the combo timer. </summary>
	/// <description>
	/// 	The base score value can only be increased. The 
	/// 	recommended way to use this is with the += operator.
	/// 	Every time this value is updated the combo timer
	/// 	restarts, preventing the combo completion. 
	/// 	When the timer stops the value is added to mult.
	/// </description>
	/// <value>The combo builder.</value>
	public float comboBuilder
	{
		get{ return _comboBuilder; }
		set
		{ 
			if( enabled )
			{
				float newVal = VerifySameOrBigger(_comboBuilder,value);
				if( newVal > _comboBuilder )
				{
					_comboBuilder = newVal;
					_comboTimer = COMBO_TIME_OUT;
				}
			}
		}
	}

	//--------------------------------------
	/// <summary> Returns the current score multiplier (readonly). </summary>
	/// <value>The multiplier.</value>
	public float multiplier
	{
		get{ return _mult; }
	}

	//--------------------------------------
	/// <summary> Returns the total score (readonly). </summary>
	/// <description> 
	/// 	The rounded down values of baseScore times the multiplier.
	/// </description>
	/// <value>The total score .</value>
	public float total
	{
		get{ return Mathf.Floor(baseScore) * Mathf.Floor(multiplier); }
	}
	#endregion


	//--------------------------------------------------------------
	#region		Private state
	//--------------------------------------
	private float _baseScore;
	private float _mult;
	private float _comboBuilder;
	private float _comboTimer;
	#endregion


	//--------------------------------------------------------------
	/// <summary> Initialise this instance. </summary>
	//--------------------------------------
	void Start()
	{
		OnComboCompleted += HandleOnComboCompleted;
		Reset();
	}

	//--------------------------------------------------------------
	/// <summary> Update this instance. </summary>
	//--------------------------------------
	void Update()
	{
		if( _comboTimer > 0 )
		{
			_comboTimer -= Time.deltaTime;

			if( _comboTimer<= 0 )
			{
				float comboScore = GetComboScore(_comboBuilder);
                if (OnComboCompleted != null)
                {
                    ScoreEventArgs eventArgs = new ScoreEventArgs(this, comboScore);
                    OnComboCompleted(this, eventArgs);
                }
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Gets a reduced comboed score. </summary>
	/// <description>
	/// 	Comboes are encouraged by awarding larger sequences of
	/// 	actions with higher score. This function takes a
	/// 	raw score, clamps it (0 to 10) and reduces it as
	/// 	appropriated.
	/// </description>
	/// <returns>A combo score.</returns>
	/// <param name="rawComboScore">Raw score to be comboed.</param>
	//--------------------------------------
	static float GetComboScore( float rawComboScore )
	{
		float combo01;

		combo01	= rawComboScore / MAX_COMBO_POINTS;
		combo01 = Mathf.Clamp01(combo01);
		combo01 = Mathf.Pow( combo01, COMBO_EFFECT );
		
		return combo01 * MAX_COMBO_POINTS;
	}

	//--------------------------------------------------------------
	/// <summary> Handles the combo completion event. </summary>
	/// <param name="score">The score instance.</param>
	/// <param name="comboPoints">Combo points.</param>
	//--------------------------------------
	void HandleOnComboCompleted(object sender, ScoreEventArgs combo)
	{
		_mult += combo.points;
		_comboBuilder = 0;
	}
	
	//--------------------------------------------------------------
	/// <summary> Resets the score. </summary>
	//--------------------------------------
	private void Reset()
	{
		_baseScore	= 0;
		_mult		= 1;
		_comboBuilder	= 0;
		_comboTimer	= 0;
	}

	//--------------------------------------------------------------
	/// <summary> An utility function to raise an exception
	/// if the newVal is smaller than oldVal </summary>
	/// <returns>The new value.</returns>
	/// <param name="oldVal">Old value.</param>
	/// <param name="newVal">New value.</param>
	//--------------------------------------
	private static float VerifySameOrBigger(float oldVal, float newVal)
	{
		if( newVal < oldVal )
			throw new System.ArgumentOutOfRangeException(
				"The new assigned value cannot be lower.");
		return newVal;
	}
}

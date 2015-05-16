/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                         ScoreLine.cs                        </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    28-Apr-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLine : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Inspector settings
	//--------------------------------------
	[SerializeField] Text _rankText     = null;
	[SerializeField] Text _userNameText = null;
	[SerializeField] Text _scoreText    = null;
	[SerializeField] Text _timeText     = null;
	#endregion


	//--------------------------------------------------------------
	#region Public properties
	//--------------------------------------
	public string rank
	{
		get{ return _rankText.text;  }
		set{ _rankText.text = value; }
	}

	public string userName
	{
		get{ return _userNameText.text;  }
		set{ _userNameText.text = value; }
	}

	public string score
	{
		get{ return _scoreText.text;  }
		set{ _scoreText.text = value; }
	}

	public string time
	{
		get{ return _timeText.text;  }
		set{ _timeText.text = value; }
	}
	#endregion
}

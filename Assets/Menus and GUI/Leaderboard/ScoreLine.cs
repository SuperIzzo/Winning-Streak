using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreLine : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Inspector settings
	//--------------------------------------
	[SerializeField] Text _rankText;
	[SerializeField] Text _nameText;
	[SerializeField] Text _scoreText;
	[SerializeField] Text _timeText;
	#endregion


	//--------------------------------------------------------------
	#region Public properties
	//--------------------------------------
	public string rank
	{
		get{ return _rankText.text;  }
		set{ _rankText.text = value; }
	}

	public string name
	{
		get{ return _nameText.text;  }
		set{ _nameText.text = value; }
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

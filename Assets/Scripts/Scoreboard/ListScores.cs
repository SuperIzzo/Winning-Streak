using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ListScores : MonoBehaviour {

	TextMesh text;
	List<string> scoreList;
	List<int> scoreListInt = new List<int>();

	// Use this for initialization
	void Start () 
	{
		text = this.GetComponent<TextMesh>();

		int currentScore = 3;//ScoreManager.score;

		for(int i = 0; i < 5; i++)
		{
			string str = "Score" + i;
			string prefStr = PlayerPrefs.GetString(str);

			scoreListInt.Add (System.Int32.Parse(prefStr));
		}


		for(int i = 0; i < 5; i++)
		{
			if(currentScore > scoreListInt[i])
			{
				if(i + 4 < 5) scoreListInt[i+4] = scoreListInt[i+3];
				if(i + 3 < 5) scoreListInt[i+3] = scoreListInt[i+2];
				if(i + 2 < 5) scoreListInt[i+2] = scoreListInt[i+1];
				if(i + 1 < 5) scoreListInt[i+1] = scoreListInt[i];

				scoreListInt[i] = currentScore;

				//skip the rest
				i = 6;
			}
		}

		text.text += "\n";

		for(int i = 0; i < 5; i++)
		{
			text.text += i+1 + ": " + scoreListInt[i] + "\n";
		}

		for(int i = 0; i < 5; i++)
		{
			PlayerPrefs.SetString("Score" + i, scoreListInt[i].ToString());
		}
	}
	

}

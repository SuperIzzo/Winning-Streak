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

		int currentScore = ScoreManager.score;

		for(int i = 0; i < 5; i++)
		{
			string str = "Score" + i;
			string prefStr = PlayerPrefs.GetString(str);

			scoreListInt.Add (System.Int32.Parse(prefStr));
		}


		for(int i = 0; i < 5; i++)
		{
			Debug.Log (scoreListInt[i]);

		}
	}
	

}

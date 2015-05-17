/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                     JSON_Persistence.cs                     </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Jake Thorne                                            </author> * 
 * <date>    12-May-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using SimpleJSON;

//-------------------------------------------------------------
/// <summary> Class to load in (and then save) any persistent
/// data in the game. </summary>
//-------------------------------------------------------------

public class JSON_Persistence : MonoBehaviour {

    //Where to find the .json file
    const string dataPath = "JSON/AchievementData";

    //corresponding data files in the .json file
    const string achievementCountValue = "amount";
    const string achievementListValue = "achievementNames";
    const string achievementArrayValue = "data";

    const string achieveIDValue = "id";
    const string achieveDescriptionValue = "description";
    const string achieveTextureValue = "texture";




    //List to store the loaded data into
    private List<Achievement> AchievementData = new List<Achievement>();

    public void Load()
    {
        LoadAchievements();
        LoadScore();
    }

    public void Save()
    {
        string path = null;
         #if UNITY_EDITOR
         path = "Assets/Resources/JSON/ItemInfo.json";
         #endif
         //#if UNITY_STANDALONE
             // You cannot add a subfolder, at least it does not work for me
         //path = "WinningStreak_Data/Resources/ItemInfo.json";
         //#endif

         SimpleJSON.JSONNode node = SimpleJSON.JSONNode.Parse(Resources.Load<TextAsset>
                                                              (dataPath).text);
  
         string str = node.ToString();
         using (FileStream fs = new FileStream(path, FileMode.Create)){
             using (StreamWriter writer = new StreamWriter(fs)){
                 writer.Write(str);
             }
         }
         #if UNITY_EDITOR
         UnityEditor.AssetDatabase.Refresh();
         #endif
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    
    void LoadScore()
    {

    }

    //-------------------------------------------------------------
    /// <summary> Tests the loaded in .JSON file to see if
    /// referenced data is valid within the file. </summary>
    /// <param name="_node">The JSONNode file load in to test.</param>
    /// <param name="_value">The value to test for validity.</param>
    //-------------------------------------------------------------
    bool IsDataValid(JSONNode _node, string _value)
    {
        if (_node[_value] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find node: '" + _value + "'.");
        }

        return true;
    }

    #region Score specific functions


    #endregion
    #region Achievement specific functions

    void LoadAchievements()
    {
        //Load in the .json file from the data path
        SimpleJSON.JSONNode node = SimpleJSON.JSONNode.Parse(Resources.Load<TextAsset>
                                                             (dataPath).text);

        //exit if the .json file isn't valid (missing data/incorrectly formatted)
        if (!CheckAchievementValidity(node))
        {
            return;
        }

        //Start data load in
        int dataCount = node[achievementCountValue].AsInt;
        for (int i = 0; i < dataCount; i++)
        {
            //Make sure that the achievement to load has all required data
            if (!IsAchievementValid(node, node[achievementListValue][i]))
                return;

            //Load the data in
            Achievement temp_achievement = new Achievement();

            temp_achievement.name = node[achievementListValue][i];

            temp_achievement.id =
                node[achievementArrayValue][temp_achievement.name][achieveIDValue].AsInt;

            temp_achievement.description =
                node[achievementArrayValue][temp_achievement.name][achieveDescriptionValue];

            temp_achievement.texture =
                node[achievementArrayValue][temp_achievement.name][achieveTextureValue];

            AchievementData.Add(temp_achievement);
        }
    }

    //-------------------------------------------------------------
    /// <summary> Tests the loaded in .JSON file against all value
    /// paths to validate the file. </summary>
    /// <param name="_node">The JSONNode file load in to test.</param>
    //-------------------------------------------------------------
    bool CheckAchievementValidity(JSONNode _node)
    {
        //Amount value check
        if (!IsDataValid(_node, achievementCountValue)) return false;

        //achievement list check
        if (!IsDataValid(_node, achievementListValue)) return false;

        //achievement data check
        if (!IsDataValid(_node, achievementArrayValue)) return false;

        return true;
    }


    //-------------------------------------------------------------
    /// <summary> Tests the achievement from the .json file to see
    /// that they have the required data. </summary>
    /// <param name="_node">The JSONNode file load in to test.</param>
    /// <param name="_name">The achievement to test for validity.</param>
    //-------------------------------------------------------------
    bool IsAchievementValid(JSONNode _node, string _name)
    {
        if (_node[achievementArrayValue][_name] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement: '"
                + _name + "'.");
        }

        if (_node[achievementArrayValue][_name][achieveIDValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '"
                + _name + ":" + achieveIDValue + "'.");
        }

        if (_node[achievementArrayValue][_name][achieveDescriptionValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '"
                + _name + ":" + achieveDescriptionValue + "'.");
        }

        if (_node[achievementArrayValue][_name][achieveTextureValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '"
                + _name + ":" + achieveTextureValue + "'.");
        }

        return true;
    }
    #endregion
}

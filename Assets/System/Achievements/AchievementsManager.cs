using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;

public class AchievementsManager : MonoBehaviour
{
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
    public List<Achievement> AchievementData;

    void Start()
    {
        LoadAchievements();
    }

    //-------------------------------------------------------------
    /// <summary> Loads the achievements data from the JSON
    /// file: "Assets/Resources/JSON/AchievementData.json" </summary>
    //-------------------------------------------------------------
    void LoadAchievements()
    {
        //Load in the .json file from the data path
        SimpleJSON.JSONNode node = SimpleJSON.JSONNode.Parse(Resources.Load<TextAsset>
                                                             (dataPath).text);

        //exit if the .json file isn't valid (missing data/incorrectly formatted)
        if (!CheckValidity(node))
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
    bool CheckValidity(JSONNode _node)
    {
        //Amount value check
        if (!IsDataValid(_node, achievementCountValue)) return false;

        //achievement list check
        if (!IsDataValid(_node, achievementListValue))  return false;
        
        //achievement data check
        if (!IsDataValid(_node, achievementArrayValue)) return false;

        return true;
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

            return false;
        }

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

            return false;
        }

        if (_node[achievementArrayValue][_name][achieveIDValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '" 
                + _name + ":" + achieveIDValue + "'.");

            return false;
        }

        if (_node[achievementArrayValue][_name][achieveDescriptionValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '" 
                + _name + ":" + achieveDescriptionValue + "'.");

            return false;
        }

        if (_node[achievementArrayValue][_name][achieveTextureValue] == null)
        {
            throw new MissingReferenceException(
                "AchievementData.json format error. could not find achievement data: '" 
                + _name + ":" + achieveTextureValue + "'.");

            return false;
        }

        return true;
    }
}

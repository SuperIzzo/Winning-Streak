using UnityEngine;
using System.Collections;

namespace RoaringSnail.PersistenceSystems {

//-------------------------------------------------------------
/// <summary> Persistance using PlayerPrefs. </summary>
//--------------------------------------
public class PrefsPersistence : IPersistence
{
	//-------------------------------------------------------------
	/// <summary> Removes all keys and values from the preferences.
	///           Use with caution. </summary>
	//--------------------------------------
	public void DeleteAll()
	{
		PlayerPrefs.DeleteAll();
	}

	//-------------------------------------------------------------
	/// <summary> Removes key and its corresponding value
	///           from the preferences. </summary>
	/// <param name="key">The key to be removed.</param>
	//--------------------------------------
	public void DeleteKey(string key)
	{
		PlayerPrefs.DeleteKey(key);
	}

	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  preference file if it exists. </summary>
	/// <returns>The float.</returns>
	/// <param name="key">Key.</param>
	/// <param name="defaultValue">Default value.</param>
	//--------------------------------------
	public float GetFloat(string key, float defaultValue = 0.0F)
	{
		return PlayerPrefs.GetFloat(key, defaultValue );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Returns the value corresponding to key in the 
	///  persistent data if it exists.
	/// </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	public int GetInt(string key, int defaultValue = 0)
	{
		return PlayerPrefs.GetInt( key, defaultValue );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Returns the value corresponding to key in the 
	///  persistent data if it exists.
	/// </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	public string GetString(string key, string defaultValue = "")
	{
		return PlayerPrefs.GetString( key, defaultValue );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Returns true if key exists in the
	///  persistent data.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	/// <param name="key">Key.</param>
	//--------------------------------------
	public bool HasKey(string key)
	{
		return PlayerPrefs.HasKey( key );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Save this instance.
	/// </summary>
	/// <description>Some implementations may wait for convenient time before
	/// writing the data to a safe location and if the game crashes
	/// all progress reported may be lost. This function hints at
	/// an appropriate time for the progress to be saved.</description>
	//--------------------------------------
	public void Save()
	{
		PlayerPrefs.Save();
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Sets the value of the preference identified by
	///  the specified key.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat( key, value );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Sets the value of the preference identified by
	///  the specified key.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt( key, value );
	}

	//-------------------------------------------------------------
	/// <summary>
	/// Sets the value of the preference identified by
	///  the specified key.
	/// </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public void SetString(string key, string value)
	{
		PlayerPrefs.SetString( key, value );
	}
}

}

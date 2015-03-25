using UnityEngine;
using System.Collections;
using RoaringSnail.PersistenceSystems;

//-------------------------------------------------------------
/// <summary> Stores and aceeses persistent game data,
/// 		  between game sessions. </summary>
/// <description>
/// 	The class has the same interface as the Unity class
/// <see href= "http://docs.unity3d.com/ScriptReference/PlayerPrefs.html">
/// PlayerPrefs</see>, but its implementation may vary based on
/// the needs of the game. The intended of Persistence use is
/// to store saved games data, for player settings and
/// preferences use PlayerPrefs instead.
/// </description>
//--------------------------------------
public class Persistence
{
	//-------------------------------------------------------------
	/// <summary> This is the currently active
	///           persistence manager. </summary>
	/// <value>The active persistence.</value>
	//--------------------------------------
	public static IPersistence Active {get; set;}

	//-------------------------------------------------------------
	/// <summary> Removes all keys and values from the 
	/// 		  persistent data. Use with caution. </summary>
	//--------------------------------------
	public static void DeleteAll()
	{
		CeckActive();
		Active.DeleteAll();
	}
	
	//-------------------------------------------------------------
	/// <summary> Removes key and its corresponding value
	///           from the persistent data. </summary>
	/// <param name="key">The key to be removed.</param>
	//--------------------------------------
	public static void DeleteKey(string key)
	{
		CeckActive();
		Active.DeleteKey(key);
	}
	
	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	public static float GetFloat(string key, float defaultValue = 0.0F)
	{
		CeckActive();
		return Active.GetFloat(key, defaultValue );
	}
	
	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	public static int GetInt(string key, int defaultValue = 0)
	{
		CeckActive();
		return Active.GetInt( key, defaultValue );
	}
	
	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	public static string GetString(string key, string defaultValue = "")
	{
		CeckActive();
		return Active.GetString( key, defaultValue );
	}
	
	//-------------------------------------------------------------
	/// <summary> Returns true if key exists in the
	/// 		  persistent data. </summary>
	/// <returns><c>true</c> if the persistent data has the 
	///          specified key; otherwise, <c>false</c>.</returns>
	/// <param name="key">Key.</param>
	//--------------------------------------
	public static bool HasKey(string key)
	{
		CeckActive();
		return Active.HasKey( key );
	}
	
	//-------------------------------------------------------------
	/// <summary> Save this instance. </summary>
	/// <description>
	/// Some implementations may wait for convenient time before
	/// writing the data to a safe location and if the game crashes
	/// all progress reported may be lost. Use this function to
	/// hint at appropriate times for the progress to be saved.
	/// Use with caution as this may result in potential hiccups.
	/// </description>
	//--------------------------------------
	public static void Save()
	{
		CeckActive();
		Active.Save();
	}
	
	//-------------------------------------------------------------
	/// <summary> Sets the value of the preference identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public static void SetFloat(string key, float value)
	{
		CeckActive();
		Active.SetFloat( key, value );
	}
	
	//-------------------------------------------------------------
	/// <summary> Sets the value of the preference identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public static void SetInt(string key, int value)
	{
		CeckActive();
		Active.SetInt( key, value );
	}
	
	//-------------------------------------------------------------
	/// <summary> Sets the value of the preference identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	public static void SetString(string key, string value)
	{
		CeckActive();
		Active.SetString( key, value );
	}
	
	//-------------------------------------------------------------
	/// <summary> Tests if the Active instance is valid </summary>
	//--------------------------------------
	private static void CeckActive()
	{
		if( Active == null )
			throw new MissingReferenceException( 
			    "Persistence.Active is not set to a valid " +
				"instance of IPersistance." );
	}
}
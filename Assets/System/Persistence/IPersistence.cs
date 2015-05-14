namespace RoaringSnail.PersistenceSystems {

//-------------------------------------------------------------
/// <summary> A generic Persistence interface, which
///	      implementation must inherit. </summary>
//--------------------------------------
public interface IPersistence 
{
	//-------------------------------------------------------------
	/// <summary> Removes all keys and values from the 
	/// 		  persistent data. Use with caution. </summary>
	//--------------------------------------
	void DeleteAll();

	//-------------------------------------------------------------
	/// <summary> Removes key and its corresponding value
	///           from the persistent data. </summary>
	/// <param name="key">The key to be removed.</param>
	//--------------------------------------
	void DeleteKey(string key);

	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The float.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	float GetFloat(string key, float defaultValue = 0.0F);

	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The int.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	int GetInt(string key, int defaultValue = 0);

	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///			  persistent data if it exists. </summary>
	/// <returns>The string.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	string GetString(string key, string defaultValue = "");

	//-------------------------------------------------------------
	/// <summary> Returns the value corresponding to key in the 
	///		persistent data if it exists. </summary>
	/// <returns>The data.</returns>
	/// <param name="key">The key.</param>
	/// <param name="defaultValue">A default value to be returned
	/// if the key could not be found.</param>
	//--------------------------------------
	T GetObject<T>( string key, T defaultValue = null ) where T : class;

	//-------------------------------------------------------------
	/// <summary> Returns true if key exists in the
	/// 		  persistent data. </summary>
	/// <returns><c>true</c> if the persistent data has the 
	///          specified key; otherwise, <c>false</c>.</returns>
	/// <param name="key">Key.</param>
	//--------------------------------------
	bool HasKey(string key);

	//-------------------------------------------------------------
	/// <summary> Save this instance. </summary>
	/// <description>
	/// Some implementations may wait for convenient time before
	/// writing the data to a safe location and if the game crashes
	/// all progress reported may be lost. This function hints at
	/// an appropriate time for the progress to be saved.
	/// </description>
	//--------------------------------------
	void Save();

	//-------------------------------------------------------------
	/// <summary> Sets the value of the persistence identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	void SetFloat(string key, float value);

	//-------------------------------------------------------------
	/// <summary> Sets the value of the persistence identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	void SetInt(string key, int value);

	//-------------------------------------------------------------
	/// <summary> Sets the value of the persistence identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	void SetString(string key, string value);
	
	//-------------------------------------------------------------
	/// <summary> Sets the value of the persistence identified by
	/// 		  the specified key. </summary>
	/// <param name="key">Key.</param>
	/// <param name="value">Value.</param>
	//--------------------------------------
	void SetObject<T>( string key, T value) where T : class;
}


}  // namespace RoaringSnail.PeristenceSystems
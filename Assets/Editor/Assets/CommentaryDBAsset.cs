using UnityEngine;
using UnityEditor;

public class CommentaryDBAsset
{
	[MenuItem("Assets/Create/Database/Commentary")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<CommentaryDB> ();
	}
}
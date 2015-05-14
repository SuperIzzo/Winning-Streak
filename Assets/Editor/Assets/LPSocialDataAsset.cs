using UnityEditor;
using RoaringSnail.SocialPlatforms.LocalImpl;

public class LPSocialDataAsset
{
	[MenuItem("Assets/Create/Database/Local Social Data")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<SocialConfiguration> ();
	}
}

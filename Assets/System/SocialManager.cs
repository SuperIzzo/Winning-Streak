#define DEBUG_SOCIAL

using UnityEngine;
using LP = RoaringSnail.SocialPlatforms.LocalImpl;


//--------------------------------------------------------------
/// <summary> A high level social manager that does the
///           initializatio work for the SocailAPI. </summary>
//--------------------------------------
public class SocialManager : MonoBehaviour
{
	public LP.SocialConfiguration localSocialData;



    //--------------------------------------------------------------
    /// <summary> Initializes the SocialManager </summary>
    //--------------------------------------
    void Start ()
	{
        LP.SocialPlatform.Activate( localSocialData );
		Social.localUser.Authenticate( SocialAuthentication );
	}



    //--------------------------------------------------------------
    /// <summary> Reports authentication success </summary>
    /// <param name="success"></param>
    //--------------------------------------
    void SocialAuthentication( bool success )
	{
        #if DEBUG_SOCIAL
            if ( success )
		    {
                Debug.Log("Social authentication successful");
		    }
		    else
		    {
			    Debug.Log( "Social authentication failed" );
		    }
		#endif
	}
}

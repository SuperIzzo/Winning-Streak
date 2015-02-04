using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace LocalPlatform
{
	class UserProfile : IUserProfile
	{
		// Private state
		private string 			_userName = "";
		private string			_id	= "";
		private bool			_isFriend = false;
		private UserState 		_state = UserState.Offline;
		private Texture2D		_image;
		
		// Public properties
		public string 			userName{ get{ return _userName; } }
		public string			id{ get{ return _id; } }
		public bool				isFriend{ get{ return _isFriend; } }
		public UserState		state{ get{ return _state; } }
		public Texture2D		image{ get{ return _image; } }
	}
}
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace LocalPlatform
{
	class LocalUser : UserProfile, ILocalUser
	{
		private bool		_authenticated = false;
		private bool		_underage = false;
		
		
		// Local user has no friends
		public IUserProfile[] 	friends{ get { return new IUserProfile[]{}; } }
		public bool 			authenticated{ get{ return _authenticated; } }
		public bool 			underage{ get{ return _underage; } }
		
		public void Authenticate( System.Action<bool> callback )
		{
			_authenticated = true;
			_underage = false;
			
			callback( true );
		}
		
		public void LoadFriends( System.Action<bool> callback )
		{
			throw new System.NotImplementedException();
			
			callback( true );
		}
	}
}
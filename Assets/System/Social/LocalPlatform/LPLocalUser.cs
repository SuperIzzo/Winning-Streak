using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace RoaringSnail.SocialPlatforms.LocalImpl
{
    class LocalUser : UserProfile, ILocalUser
    {
        // Local user has no friends
        public IUserProfile[] friends { get { return new IUserProfile[] { }; } }
        public bool authenticated { get; protected set; }
        public bool underage { get; protected set; }


        public LocalUser(string userID)
        {
            authenticated = false;
            underage = false;
            id = userID;
        }

        public void Authenticate(System.Action<bool> callback)
        {
            authenticated = true;
            underage = false;

            callback(true);
        }

        public void LoadFriends(System.Action<bool> callback)
        {
            throw new System.NotImplementedException();

            callback(true);
        }
    }
}

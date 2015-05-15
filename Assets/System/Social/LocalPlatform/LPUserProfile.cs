using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace RoaringSnail.SocialPlatforms.LocalImpl
{
    class UserProfile : IUserProfile
    {
        // Public properties
        public string userName { get; protected set; }
        public string id { get; protected set; }
        public bool isFriend { get; protected set; }
        public UserState state { get; protected set; }
        public Texture2D image { get; protected set; }

        public UserProfile()
        {
            userName = "";
            id = "";
            isFriend = false;
            state = UserState.Offline;
            image = null;
        }
    }
}

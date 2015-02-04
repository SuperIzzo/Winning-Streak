using UnityEngine;
using UnityEngine.SocialPlatforms;
using System.Collections;

namespace LocalPlatform
{
	public class SocialPlatform : ISocialPlatform
	{
		private static SocialPlatform 	_instance;
		private LocalUser 				_localUser;

		public ILocalUser localUser
		{
			get
			{
				if( _localUser == null )
				{
					_localUser = new LocalUser();
				}

				return _localUser;
			}
		}

		public static void Activate()
		{
			if( _instance == null )
			{
				_instance = new SocialPlatform();
			}

			Social.Active = _instance;
		}

		public void Authenticate( ILocalUser user, System.Action<bool> callback )
		{
			user.Authenticate( callback );


			Debug.LogWarning( "SocialPlatform.Authenticate not implemented");
			//Application.persistentDataPath
		}

		public void LoadUsers( string[] userIds, System.Action<IUserProfile[]> callback )
		{
			throw new System.NotImplementedException();

			IUserProfile[] profiles = new IUserProfile[]{};

			callback( profiles );
		}

		public void LoadFriends( ILocalUser user, System.Action<bool> callback )
		{
			user.LoadFriends( callback );
		}

		public void LoadScores( string board, System.Action<IScore[]> callback )
		{
			throw new System.NotImplementedException();

			IScore[] scores = new IScore[]{};

			callback( scores );
		}

		public void LoadScores( ILeaderboard board, System.Action<bool> callback )
		{
			board.LoadScores( callback );
		}

		public bool GetLoading( ILeaderboard board )
		{
			return board.loading;
		}

		public void LoadAchievements( System.Action<IAchievement[]> callback )
		{
			throw new System.NotImplementedException();
			
			IAchievement[] achievements = new IAchievement[] {};
			
			callback( achievements );
		}

		public void LoadAchievementDescriptions( System.Action<IAchievementDescription[]> callback )
		{
			throw new System.NotImplementedException();

			IAchievementDescription[] descs = new IAchievementDescription[]{};

			callback( descs );
		}

		public ILeaderboard CreateLeaderboard()
		{
			throw new System.NotImplementedException();
			
			return null;
		}

		public IAchievement CreateAchievement()
		{
			throw new System.NotImplementedException();

			return null;
		}

		public void ReportScore( long score, string board, System.Action<bool> callback )
		{
			throw new System.NotImplementedException();

			callback( true );
		}

		public void ReportProgress( string id, double progress, System.Action<bool> callback )
		{
			throw new System.NotImplementedException();
			
			callback( true );
		}

		public void ShowLeaderboardUI()
		{
			throw new System.NotImplementedException();
		}

		public void ShowAchievementsUI()
		{
			throw new System.NotImplementedException();
		}
	}

}
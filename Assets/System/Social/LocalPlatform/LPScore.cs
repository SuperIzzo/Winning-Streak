using UnityEngine;
using UnityEngine.SocialPlatforms;
using System;

namespace RoaringSnail.SocialPlatforms.LocalImpl
{
	[Serializable]
	public class Score : IScore 
	{
		public DateTime date
		{
			get;
			set;
		}
		public string leaderboardID
		{
			get;
			set;
		}
		public long value
		{
			get;
			set;
		}
		public string formattedValue
		{
			get;
			private set;
		}
		public string userID
		{
			get;
			set;
		}
		public int rank
		{
			get;
			set;
		}

		public void ReportScore(Action<bool> callback)
		{
			throw new NotImplementedException();
		}
	}
}
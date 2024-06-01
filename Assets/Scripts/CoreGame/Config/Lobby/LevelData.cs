using System;
using UnityEngine;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public class LevelData
	{
		public string Id;
		public LevelType Type;
		public GameObject ViewPrefab;
		public string EnemyId;
		public RewardData[] Rewards;
	}
}
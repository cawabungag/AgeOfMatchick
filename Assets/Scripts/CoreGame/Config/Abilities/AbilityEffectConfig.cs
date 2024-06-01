using System;
using UnityEngine;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public class AbilityEffectConfig
	{
		public AbilityEffectType Type;
		public int Duration;
		public int Value;
		[Range(0, 100)]
		public float Chance;

		public BattleManager.AbilityEffect Clone()
		{
			return new BattleManager.AbilityEffect()
			{
				Type = Type,
				Duration = Duration,
				Value = Value,
			};
		}
	}
}
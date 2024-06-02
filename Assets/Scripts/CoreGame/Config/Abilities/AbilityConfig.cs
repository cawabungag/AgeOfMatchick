using System;
using UnityEngine;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public class AbilityConfig
	{
		public AbilityVisualData VisualData;
		public AbilityEffectConfig[] ThreeMatchEffectsIds;
		public AbilityEffectConfig[] FourMatchEffectsIds;
		public AbilityEffectConfig[] FiveMatchEffectsIds;
		public AudioClip AudioClip;
	}
}
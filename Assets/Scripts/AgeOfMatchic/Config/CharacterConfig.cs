using System;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public class CharacterConfig
	{
		public string CharacterId;
		public CharacterVisualData VisualData;
		public AbilityConfig Ability;
		public BoosterAbilityConfig BoosterAbility;
		public CharacterStatsConfig ChatacterStatsConfig;
		public bool IsEnemy;
	}
}
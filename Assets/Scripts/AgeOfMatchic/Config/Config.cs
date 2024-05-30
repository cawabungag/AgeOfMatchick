using System;
using UnityEngine;

namespace AgeOfMatchic.Config
{
	[CreateAssetMenu(menuName = "Config", fileName = "Config")]
	public class ConfigObject : ScriptableObject
	{
		[SerializeField]
		private Config _config;
	}
	
	[Serializable]
	public class Config
	{
		public CharacterConfig[] Characters;
	}
	
	[Serializable]
	public class CharacterConfig
	{
		public string CharacterId;
		public CharacterVisualData VisualData;
		public AbilityConfig Ability;
		public BoosterAbilityConfig BoosterAbility;
	}
	
	[Serializable]
	public class CharacterVisualData
	{
		public string Name;
		public string Description;
		public Sprite Sprite;
	}

	[Serializable]
	public class BoosterAbilityConfig
	{
		public AbilityVisualData VisualData;
		public AbilityEffectConfig[] Effects;
	}

	[Serializable]
	public class AbilityConfig
	{
		public AbilityVisualData VisualData;
		public AbilityEffectConfig[] ThreeMatchEffectsIds;
		public AbilityEffectConfig[] FourMatchEffectsIds;
		public AbilityEffectConfig[] FiveMatchEffectsIds;
	}

	[Serializable]
	public class AbilityVisualData
	{
		public string Name;
		public string Description;
		public Sprite Icon;
	}

	[Serializable]
	public class AbilityEffectConfig
	{
		public AbilityEffectType Type;
		public int Duration;
		public int Value;
		[Range(0, 100)]
		public float Chance;
	}

	[Serializable]
	public enum AbilityEffectType
	{
		//Abilities
		DamageHealth,
		DamageShield,
		HealHealth,
		HealShield,
		Reflect,
		Stun,
		Shuffle,
		ShuffleWithDamage,
		
		//Busters
		DeleteRow,
		DoubleEffect,
		SilverShuffle
	}
}
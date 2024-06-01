using System;

namespace AgeOfMatchic.Config
{
	[Serializable]
	public enum AbilityEffectType
	{
		//Abilities
		DamageHealth,
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
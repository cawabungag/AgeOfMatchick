using System.Collections.Generic;
using System.Linq;
using AgeOfMatchic.Config;
using Common;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public class AbilityEffect
    {
        public AbilityEffectType Type;
        public int Duration;
        public int Value;
        public float Chance;

        public AbilityEffectConfig ToAbilityEffectConfig()
        {
            return new AbilityEffectConfig()
            {
                Type = Type,
                Duration = Duration,
                Value = Value,
                Chance = Chance
            };
        }
    }

    private readonly Dictionary<bool, List<AbilityEffect>> Buffs = new()
    {
        {true, new List<AbilityEffect>()},
        {false, new List<AbilityEffect>()},
    };

    private void ApplyAbilityEffect(AbilityEffectConfig origEffect, bool applyToEnemy)
    {
        if (origEffect.Chance < 100 && !CheckChance(origEffect.Chance))
        {
            return;
        }
        
        if (origEffect.Duration > 0)
        {
            if (Buffs[applyToEnemy].Any(x => x.Type == origEffect.Type))
            {
                var index = Buffs[applyToEnemy].FindIndex(x => x.Type == origEffect.Type);
                Buffs[applyToEnemy][index] = new AbilityEffect
                {
                    Type = origEffect.Type,
                    Duration = origEffect.Duration,
                    Value = origEffect.Value,
                    Chance = origEffect.Chance
                };
            }
            else
            {
                Buffs[applyToEnemy].Add(new AbilityEffect
                {
                    Type = origEffect.Type,
                    Duration = origEffect.Duration,
                    Value = origEffect.Value,
                    Chance = origEffect.Chance
                });
                return;
            }
        }

        switch (origEffect.Type)
        {
            case AbilityEffectType.DamageHealth:
                if (applyToEnemy)
                {
                    BattleUi.Instance.ApplyDamageToEnemy(origEffect.Value);
                    break;
                }
                
                BattleUi.Instance.ApplyDamageToAlly(origEffect.Value);
                break;
            
            case AbilityEffectType.HealHealth:
                BattleUi.Instance.HealHealth(origEffect.Value);
                break;
            case AbilityEffectType.HealShield:
                BattleUi.Instance.HealShield(origEffect.Value);
                break;
            case AbilityEffectType.Reflect:
                // Implement Reflect logic here
                break;
            case AbilityEffectType.Stun:
                // Implement Stun logic here
                break;
            // Add more cases as needed
            case AbilityEffectType.Shuffle:
                break;
            case AbilityEffectType.ShuffleWithDamage:
                break;
            case AbilityEffectType.DeleteRow:
                break;
            case AbilityEffectType.DoubleEffect:
                break;
            case AbilityEffectType.SilverShuffle:
                break;
        }
    }

    public void ApplyBuffsAndDebuffs(bool isApplyToEnemy)
    {
        foreach (var buff in Buffs[isApplyToEnemy])
        {
            ApplyAbilityEffect(buff.ToAbilityEffectConfig(), isApplyToEnemy);
            buff.Duration--;
        }

        Buffs[isApplyToEnemy].RemoveAll(b => b.Duration <= 0);
    }
    
    private bool CheckChance(float chancePercentage)
    {
        var randomValue = Random.Range(0f, 100f);
        return randomValue < chancePercentage;
    }

    public void EnemyTurn()
    {
        var config = BattleUi.Instance.Config.Config.Characters.ToList()
            .Find(x => x.CharacterId == BattleUi.EnemyHeroes);
        foreach (var ability in config.Ability.ThreeMatchEffectsIds)
        {
            ApplyAbilityEffect(ability, false);
        }
    }
    
    public void AllyTurn(string ability, int matchCount)
    {
        var config = BattleUi.Instance.Config.Config.Characters.ToList()
            .Find(x => x.Ability.VisualData.Icon.name == ability);

        AbilityEffectConfig[] effects = null;
        
        switch (matchCount)
        {
            case 3:
                effects = config.Ability.ThreeMatchEffectsIds;
                break;
            case 4:
                effects = config.Ability.FourMatchEffectsIds;
                break;
            case 5:
                effects = config.Ability.FiveMatchEffectsIds;
                break;
        }

        if (effects == null) return;
        foreach (var effect in effects)
        {
            ApplyAbilityEffect(effect, true);
        }
    }
}

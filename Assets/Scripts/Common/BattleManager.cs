using System.Collections.Generic;
using System.Linq;
using AgeOfMatchic.Config;
using Common;
using DG.Tweening;
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

	private void ApplyAbilityEffect(AbilityEffectConfig origEffect,
		bool applyToEnemy,
		Vector3[] vector3S, Sprite ability)
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
					Move(ability, vector3S, true);
					break;
				}

				BattleUi.Instance.ApplyDamageToAlly(origEffect.Value);
				Move(ability, vector3S, false);
				break;

			case AbilityEffectType.HealHealth:
				BattleUi.Instance.HealHealth(origEffect.Value);
				Move(ability, vector3S, false);
				break;
			case AbilityEffectType.HealShield:
				BattleUi.Instance.HealShield(origEffect.Value);
				Move(ability, vector3S, false);

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
			ApplyAbilityEffect(ability, false, null, config.Ability.VisualData.Icon);
		}
	}

	public void AllyTurn(string ability, int matchCount, Vector3[] vector3S)
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
			ApplyAbilityEffect(effect, true, vector3S, config.Ability.VisualData.Icon);
		}
	}

	[SerializeField]
	private Camera _uiCamera;
	
	public RectTransform AllyEffectTarget;
	public RectTransform EnemyEffectTarget;
	public RectTransform Center;

	public float moveDuration = 2.0f;
	void Move(Sprite sprite,
		Vector3[] initialPosition, bool isToEnemy)
	{
		if (sprite.name == "Mess_Skill")
		{
			return;
		}
		
		if (initialPosition == null)
		{
			initialPosition = new[] {_uiCamera.ScreenToWorldPoint(Center.position)};
		}
		
		var seq = DOTween.Sequence();
		foreach (var p in initialPosition)
		{
			var spriteObject = new GameObject("MovingSprite");
			spriteObject.transform.localScale = new Vector3(0.4f, 0.4f, 1);
			spriteObject.transform.position = p;
			
			var spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
			spriteRenderer.sortingOrder = 100;
			spriteRenderer.sprite = sprite;
			
			var second = _uiCamera.ScreenToWorldPoint(Center.position);
			second.z = 0;
			
			var third = _uiCamera.ScreenToWorldPoint(isToEnemy ? EnemyEffectTarget.position : AllyEffectTarget.position);
			second.z = 0;

			spriteObject.transform.DOMove(second, moveDuration).OnComplete(() =>
			{
				spriteObject.transform.DOMove(third, moveDuration)
					.OnComplete(() => Destroy(spriteObject));
			});
		}

	}
}
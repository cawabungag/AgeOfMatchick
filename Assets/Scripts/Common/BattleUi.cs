using System.Collections.Generic;
using System.Linq;
using AgeOfMatchic.Config;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
	public class BattleUi : MonoBehaviour
	{
		[Header("Ally UI Elements")]
		public Image AllyHealthImage;
		public Image AllyShieldImage;
		public Image[] AllyAvatarImages;
		public Image[] AllyAbilityIcons;
		public Image[] AllyBoosterIcons;

		[Header("Enemy UI Elements")]
		public Image EnemyHealthImage;
		public Image EnemyShieldImage;
		public Image EnemyAvatarImage;

		private int MaxAllyHealth = 100;
		private int MaxAllyShield = 100;
		private int MaxEnemyHealth = 100;
		private int MaxEnemyShield = 100;

		private int allyHealth;
		private int allyShield;
		private int enemyHealth;
		private int enemyShield;
	
		[SerializeField]
		private ConfigObject _config;
		public ConfigObject Config
		{
			get
			{
				if (_config == null)
				{
					_config = Resources.Load<ConfigObject>("Config 1");
				}
				return _config;
			}
		}

		public static BattleUi Instance;
		public static string[] AllyHeroes = {"Gaheris", "Harun", "Panurg"};
		public static string EnemyHeroes = "Enemy_Hound";

		public string[] AllyAbilities
		{
			get
			{
				var buffer = new List<string>();
				foreach (var allyHero in AllyHeroes)
				{
					Debug.LogError($"_config : {_config == null}");
					var characterConfig = _config.Config.Characters.ToList().Find(x => x.CharacterId == allyHero);
					buffer.Add(characterConfig.Ability.VisualData.Icon.name);
				}

				return buffer.ToArray();
			}
		}
		
		private void Awake()
		{
			Instance = this;
		}

		public int AllyHealth
		{
			get => allyHealth;
			set
			{
				allyHealth = Mathf.Clamp(value, 0, MaxAllyHealth);
				UpdateAllyHealthUI();
			}
		}

		public int AllyShield
		{
			get => allyShield;
			set
			{
				allyShield = Mathf.Clamp(value, 0, MaxAllyShield);
				UpdateAllyShieldUI();
			}
		}

		public int EnemyHealth
		{
			get => enemyHealth;
			set
			{
				enemyHealth = Mathf.Clamp(value, 0, MaxEnemyHealth);
				UpdateEnemyHealthUI();
			}
		}

		public int EnemyShield
		{
			get => enemyShield;
			set
			{
				enemyShield = Mathf.Clamp(value, 0, MaxEnemyShield);
				UpdateEnemyShieldUI();
			}
		}

		private void UpdateAllyHealthUI()
		{
			if (AllyHealthImage != null)
			{
				AllyHealthImage.fillAmount = (float) allyHealth / MaxAllyHealth;
			}
		}

		private void UpdateAllyShieldUI()
		{
			if (AllyShieldImage != null)
			{
				AllyShieldImage.fillAmount = (float) allyShield / MaxAllyShield;
			}
		}

		private void UpdateEnemyHealthUI()
		{
			if (EnemyHealthImage != null)
			{
				EnemyHealthImage.fillAmount = (float) enemyHealth / MaxEnemyHealth;
			}
		}

		private void UpdateEnemyShieldUI()
		{
			if (EnemyShieldImage != null)
			{
				EnemyShieldImage.fillAmount = (float) enemyShield / MaxEnemyShield;
			}
		}

		private void Start()
		{
			Setup(AllyHeroes, EnemyHeroes);
		}

		public void Setup(string[] allyHeroes, string enemy)
		{
			var allySprites = new List<Sprite>();
			var abilities = new List<Sprite>();
			var boosters = new List<Sprite>();

			foreach (var allyHero in allyHeroes)
			{
				var heroConf = _config.Config.Characters.ToList()
					.Find(x => x.CharacterId == allyHero);
				MaxAllyHealth += heroConf.ChatacterStatsConfig.Health;
				MaxAllyShield += heroConf.ChatacterStatsConfig.Shield;
				allySprites.Add(heroConf.VisualData.Sprite);
				abilities.Add(heroConf.Ability.VisualData.Icon);
				boosters.Add(heroConf.BoosterAbility.VisualData.Icon);
			}

			var enemyConf = _config.Config.Characters.ToList().Find(x => x.CharacterId == enemy);

			AllyHealth = MaxAllyHealth;
			AllyShield = MaxAllyShield;
			MaxEnemyHealth = EnemyHealth = enemyConf.ChatacterStatsConfig.Health;
			MaxEnemyShield = EnemyShield = enemyConf.ChatacterStatsConfig.Shield;

			UpdateAllyAvatars(allySprites);
			UpdateEnemyAvatar(enemyConf.VisualData.Sprite);
			UpdateAllyAbilityIcons(abilities);
			UpdateAllyBoosterIcons(boosters);
		}

		private void UpdateAllyAvatars(List<Sprite> allyHeroIDs)
		{
			for (int i = 0; i < AllyAvatarImages.Length; i++)
			{
				if (i < allyHeroIDs.Count)
				{
					// Assuming you have a method to load the sprite based on the hero ID
					AllyAvatarImages[i].sprite = allyHeroIDs[i];
					AllyAvatarImages[i].enabled = true;
				}
				else
				{
					AllyAvatarImages[i].enabled = false;
				}
			}
		}

		private void UpdateEnemyAvatar(Sprite enemyHeroID)
		{
			EnemyAvatarImage.sprite = enemyHeroID;
			EnemyAvatarImage.enabled = true;
		}

		private void UpdateAllyAbilityIcons(List<Sprite> allyAbilityIcons)
		{
			for (int i = 0; i < AllyAbilityIcons.Length; i++)
			{
				if (i < allyAbilityIcons.Count)
				{
					// Assuming you have a method to load the sprite based on the ability ID
					AllyAbilityIcons[i].sprite = allyAbilityIcons[i];
					AllyAbilityIcons[i].enabled = true;
				}
				else
				{
					AllyAbilityIcons[i].enabled = false;
				}
			}
		}

		private void UpdateAllyBoosterIcons(List<Sprite> allyBoosterIcons)
		{
			for (int i = 0; i < AllyBoosterIcons.Length; i++)
			{
				if (i < allyBoosterIcons.Count)
				{
					// Assuming you have a method to load the sprite based on the booster ID
					AllyBoosterIcons[i].sprite = allyBoosterIcons[i];
					AllyBoosterIcons[i].enabled = true;
				}
				else
				{
					AllyBoosterIcons[i].enabled = false;
				}
			}
		}
		
		public void ApplyDamageToAlly(int damage)
		{
			if (allyShield > 0)
			{
				int remainingShield = allyShield - damage;
				if (remainingShield < 0)
				{
					AllyShield = 0;
					AllyHealth += remainingShield; // remainingShield is negative here
				}
				else
				{
					AllyShield = remainingShield;
				}
			}
			else
			{
				AllyHealth -= damage;
			}
		}

		public void ApplyDamageToEnemy(int damage)
		{
			if (enemyShield > 0)
			{
				int remainingShield = enemyShield - damage;
				if (remainingShield < 0)
				{
					EnemyShield = 0;
					EnemyHealth += remainingShield; // remainingShield is negative here
				}
				else
				{
					EnemyShield = remainingShield;
				}
			}
			else
			{
				EnemyHealth -= damage;
			}
		}
		
		public void HealHealth(int heal)
		{
				AllyHealth += heal;
		}
		
		public void HealShield(int heal)
		{
			AllyShield += heal;
		}
	}
}
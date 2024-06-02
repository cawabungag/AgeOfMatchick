using System;
using System.Collections.Generic;
using System.Linq;
using AgeOfMatchic.Config;
using DefaultNamespace;
using DG.Tweening;
using TMPro;
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

		public TextMeshProUGUI _allyHealth;
		public TextMeshProUGUI _allyShield;
		public TextMeshProUGUI _enemyHealth;
		public TextMeshProUGUI _enemyShield;
		
		public TextMeshProUGUI _currentEffectAlly;
		public TextMeshProUGUI _currentEffectEnemy;
	
		[SerializeField]
		private ConfigObject _config;

		[SerializeField]
		private ResultScreen _resultScreen;
		
		[SerializeField]
		private SkillDescription _skillDescription;
		
		public ConfigObject Config
		{
			get
			{
				if (_config == null)
				{
					_config = Resources.Load<ConfigObject>("Config");
				}
				return _config;
			}
		}

		public static BattleUi Instance;
		public static string[] AllyHeroes => Profile.Instance.SelectedHeroes.ToArray();
		public static string EnemyHeroes => Profile.Instance.CurrentEnemy;

		public string[] AllyAbilities
		{
			get
			{
				var buffer = new List<string>();
				foreach (var allyHero in AllyHeroes)
				{
					var characterConfig = _config.Config.Characters.ToList().Find(x => x.CharacterId == allyHero);
					buffer.Add(characterConfig.Ability.VisualData.Icon.name);
				}

				return buffer.ToArray();
			}
		}
		
		private void Awake()
		{
			if (Instance)
			{
				return;
			}
			
			Instance = this;
			Setup(AllyHeroes, EnemyHeroes);
		}


		public int AllyHealth
		{
			get => allyHealth;
			set
			{
				allyHealth = Mathf.Clamp(value, 0, MaxAllyHealth);
				if (allyHealth == 0)
				{
					if (_resultScreen.gameObject.activeSelf)
					{
						return;
					}
					_resultScreen.gameObject.SetActive(true);
					_resultScreen.SetResult(false);
				}
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
				if (enemyHealth == 0)
				{
					if (_resultScreen.gameObject.activeSelf)
					{
						return;
					}
					_resultScreen.gameObject.SetActive(true);
					_resultScreen.SetResult(true);
				}
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

		public void Setup(string[] allyHeroes, string enemy)
		{
			var allySprites = new List<Sprite>();
			var abilities = new List<Sprite>();
			var desk = new List<string>();
			var boosters = new List<Sprite>();

			foreach (var allyHero in allyHeroes)
			{
				var heroConf = _config.Config.Characters.ToList()
					.Find(x => x.CharacterId == allyHero);
				MaxAllyHealth += heroConf.ChatacterStatsConfig.Health;
				MaxAllyShield += heroConf.ChatacterStatsConfig.Shield;
				allySprites.Add(heroConf.VisualData.Sprite);
				abilities.Add(heroConf.Ability.VisualData.Icon);
				desk.Add(heroConf.Ability.VisualData.Description);
				boosters.Add(heroConf.BoosterAbility.VisualData.Icon);
			}

			var enemyConf = _config.Config.Characters.ToList().Find(x => x.CharacterId == enemy);
			AllyHealth = MaxAllyHealth;
			AllyShield = 0;
			MaxEnemyHealth = enemyConf.ChatacterStatsConfig.Health;
			MaxEnemyShield = enemyConf.ChatacterStatsConfig.Shield;
			EnemyHealth = MaxEnemyHealth;
			MaxEnemyShield = 0;

			UpdateAllyAvatars(allySprites);
			UpdateEnemyAvatar(enemyConf.VisualData.Sprite, enemyConf.Ability.VisualData.Description);
			UpdateAllyAbilityIcons(abilities, desk);
			UpdateAllyBoosterIcons(boosters);
			
			UpdateEnemyHealthUI();
			UpdateEnemyShieldUI();
			
			UpdateAllyHealthUI();
			UpdateAllyShieldUI();
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

		private void UpdateEnemyAvatar(Sprite enemyHeroID, string desk)
		{
			EnemyAvatarImage.sprite = enemyHeroID;
			EnemyAvatarImage.enabled = true;
			var button = EnemyAvatarImage.GetComponent<Button>();
			button.onClick.RemoveAllListeners();
			button.onClick.AddListener(() => _skillDescription.Setup(enemyHeroID, desk));
		}

		private void UpdateAllyAbilityIcons(List<Sprite> allyAbilityIcons, List<string> desk)
		{
			for (int i = 0; i < AllyAbilityIcons.Length; i++)
			{
				var allyAbilityIcon = AllyAbilityIcons[i];
				if (i < allyAbilityIcons.Count)
				{
					allyAbilityIcon.sprite = allyAbilityIcons[i];
					allyAbilityIcon.enabled = true;
					var button = allyAbilityIcon.GetComponent<Button>();
					button.onClick.RemoveAllListeners();
					var i1 = i;
					button.onClick.AddListener(() => _skillDescription.Setup(allyAbilityIcons[i1], desk[i1]));
				}
				else
				{
					allyAbilityIcon.enabled = false;
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
			
			SetEffect(false, false, false, damage);
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
			
			SetEffect(true, false, false, damage);
		}
		
		public void HealHealth(int heal)
		{
			AllyHealth += heal;
			SetEffect(false, true, false, heal);
		}
		
		public void HealShield(int heal)
		{
			AllyShield += heal;
			SetEffect(false, true, true, heal);
		}

		private void LateUpdate()
		{
			_allyHealth.text = $"{AllyHealth}/{MaxAllyHealth}";
			_allyShield.text = $"{AllyShield}/{MaxAllyShield}";
			_enemyHealth.text = $"{EnemyHealth}/{MaxEnemyHealth}";
			_enemyShield.text = $"{EnemyShield}/{MaxEnemyShield}";
		}

		public float fadeDuration = 1.0f;
		public float displayDuration = 1.0f;
		private void SetEffect(bool isEnemy, bool isHeal, bool healShield, int value)
		{
			var textComponent = isEnemy ? _currentEffectEnemy : _currentEffectAlly;
			if (isHeal)
			{
				textComponent.text = $"+{value}";
				if (healShield)
				{
					textComponent.color = Color.blue;
				}
				else
				{
					textComponent.color = Color.green;
				}
			}
			else
			{
				textComponent.text = $"-{value}";
				if (healShield)
				{
					textComponent.color = Color.green;
				}
				else
				{
					textComponent.color = Color.red;
				}
			}
			
			var color = textComponent.color;
			color.a = 0;
			textComponent.color = color;

			textComponent.DOFade(1, fadeDuration).OnComplete(() =>
			{
				textComponent.DOFade(0, displayDuration);
			});
		}
	}
}
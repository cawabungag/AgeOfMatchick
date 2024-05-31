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

		[Header("Max Values")]
		public int MaxAllyHealth = 100;
		public int MaxAllyShield = 100;
		public int MaxEnemyHealth = 100;
		public int MaxEnemyShield = 100;

		private int allyHealth;
		private int allyShield;
		private int enemyHealth;
		private int enemyShield;

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

		public void Setup(int initialAllyHealth,
			int initialAllyShield,
			int initialEnemyHealth,
			int initialEnemyShield,
			string[] allyHeroIDs,
			string enemyHeroID,
			string[] allyAbilityIcons,
			string[] allyBoosterIcons)
		{
			AllyHealth = initialAllyHealth;
			AllyShield = initialAllyShield;
			EnemyHealth = initialEnemyHealth;
			EnemyShield = initialEnemyShield;

			UpdateAllyAvatars(allyHeroIDs);
			UpdateEnemyAvatar(enemyHeroID);
			UpdateAllyAbilityIcons(allyAbilityIcons);
			UpdateAllyBoosterIcons(allyBoosterIcons);
		}

		private void UpdateAllyAvatars(string[] allyHeroIDs)
		{
			for (int i = 0; i < AllyAvatarImages.Length; i++)
			{
				if (i < allyHeroIDs.Length)
				{
					// Assuming you have a method to load the sprite based on the hero ID
					AllyAvatarImages[i].sprite = LoadHeroSprite(allyHeroIDs[i]);
					AllyAvatarImages[i].enabled = true;
				}
				else
				{
					AllyAvatarImages[i].enabled = false;
				}
			}
		}

		private void UpdateEnemyAvatar(string enemyHeroID)
		{
			// Assuming you have a method to load the sprite based on the hero ID
			EnemyAvatarImage.sprite = LoadHeroSprite(enemyHeroID);
			EnemyAvatarImage.enabled = true;
		}

		private void UpdateAllyAbilityIcons(string[] allyAbilityIcons)
		{
			for (int i = 0; i < AllyAbilityIcons.Length; i++)
			{
				if (i < allyAbilityIcons.Length)
				{
					// Assuming you have a method to load the sprite based on the ability ID
					AllyAbilityIcons[i].sprite = LoadAbilitySprite(allyAbilityIcons[i]);
					AllyAbilityIcons[i].enabled = true;
				}
				else
				{
					AllyAbilityIcons[i].enabled = false;
				}
			}
		}

		private void UpdateAllyBoosterIcons(string[] allyBoosterIcons)
		{
			for (int i = 0; i < AllyBoosterIcons.Length; i++)
			{
				if (i < allyBoosterIcons.Length)
				{
					// Assuming you have a method to load the sprite based on the booster ID
					AllyBoosterIcons[i].sprite = LoadBoosterSprite(allyBoosterIcons[i]);
					AllyBoosterIcons[i].enabled = true;
				}
				else
				{
					AllyBoosterIcons[i].enabled = false;
				}
			}
		}

		private Sprite LoadHeroSprite(string heroID)
		{
			// Implement your method to load a sprite based on the hero ID
			// For example, you can load from Resources folder:
			return Resources.Load<Sprite>($"Heroes/{heroID}");
		}

		private Sprite LoadAbilitySprite(string abilityID)
		{
			// Implement your method to load a sprite based on the ability ID
			// For example, you can load from Resources folder:
			return Resources.Load<Sprite>($"Abilities/{abilityID}");
		}

		private Sprite LoadBoosterSprite(string boosterID)
		{
			// Implement your method to load a sprite based on the booster ID
			// For example, you can load from Resources folder:
			return Resources.Load<Sprite>($"Boosters/{boosterID}");
		}

		// You can use these methods to set initial values or update the UI
		private void Start()
		{
			// Optionally set initial values in Start or use the Setup method elsewhere
			Setup(MaxAllyHealth, MaxAllyShield, MaxEnemyHealth, MaxEnemyShield,
				new string[] {"Ally1", "Ally2"}, "Enemy1",
				new string[] {"Ability1", "Ability2"}, new string[] {"Booster1", "Booster2"});
		}
	}
}
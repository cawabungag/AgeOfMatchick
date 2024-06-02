using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class Profile : MonoBehaviour
	{
		public static Profile Instance;
		
		[HideInInspector]
		public List<string> CompletedLevel = new();
		
		[HideInInspector]
		public List<string> Heroes = new List<string>
		{
			"Gaheris", "Harun", "Golem"
		};
		
		[HideInInspector]
		public List<string> SelectedHeroes = new List<string>
		{
			"Gaheris", "Harun", "Golem"
		};

		[HideInInspector]
		public int Common = 0;
		
		[HideInInspector]
		public int Premium = 0;

		public string CurrentEnemy;
		private bool IsInitialized;

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
			IsInitialized = PlayerPrefs.GetInt("IsInitialized") == 1;

			if (IsInitialized)
			{
				Load();
			}
			
			IsInitialized = true;
			Save();
			PlayerPrefs.SetInt("IsInitialized", 1);
			PlayerPrefs.Save();
		}

		public void CompleteLevel(string level)
		{
			CompletedLevel.Add(level);
			Save();
		}
		
		public void AddHero(string hero)
		{
			Heroes.Add(hero);
			Save();
		}
		
		public void SetCommon(int value)
		{
			Common = value;
			Save();
		}
		
		public void SetPremium(int value)
		{
			Premium = value;
			Save();
		}

		public void Load()
		{
			var levels = PlayerPrefs.GetString("levels");
			var heroes = PlayerPrefs.GetString("heroes");
			var selected = PlayerPrefs.GetString("selected");
			
			CompletedLevel = Tiny.Json.Decode<List<string>>(levels);
			Heroes = Tiny.Json.Decode<List<string>>(heroes);
			SelectedHeroes = Tiny.Json.Decode<List<string>>(selected);
			
			Common = PlayerPrefs.GetInt("common", Common);
			Premium = PlayerPrefs.GetInt("premium", Premium);
		}

		public void Save()
		{
			var levels = Tiny.Json.Encode(CompletedLevel.ToArray());
			var heroes = Tiny.Json.Encode(Heroes.ToArray());
			var selected = Tiny.Json.Encode(SelectedHeroes.ToArray());

			PlayerPrefs.SetString("levels", levels);
			PlayerPrefs.SetString("heroes", heroes);
			PlayerPrefs.SetString("selected", selected);
			PlayerPrefs.SetInt("common", Common);
			PlayerPrefs.SetInt("premium", Premium);
			PlayerPrefs.Save();
		}
	}
}
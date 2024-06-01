using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
	public class Profile : MonoBehaviour
	{
		public static Profile Instance;
		public List<string> CompletedLevel = new();
		public List<string> Heroes = new();
		public int Common = 0;
		public int Premium = 0;

		private void Awake()
		{
			Instance = this;
			DontDestroyOnLoad(this);
			Load();
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
			CompletedLevel = JsonUtility.FromJson<List<string>>(levels) ?? new List<string>();
			Heroes = JsonUtility.FromJson<List<string>>(heroes) ?? new List<string>();
			Common = PlayerPrefs.GetInt("common", Common);
			Premium = PlayerPrefs.GetInt("premium", Premium);
		}

		public void Save()
		{
			var levels = JsonUtility.ToJson(CompletedLevel);
			var heroes = JsonUtility.ToJson(Heroes);
			PlayerPrefs.SetString("levels", levels);
			PlayerPrefs.SetString("heroes", heroes);
			PlayerPrefs.SetInt("common", Common);
			PlayerPrefs.SetInt("premium", Premium);
			PlayerPrefs.Save();
		}
	}
}
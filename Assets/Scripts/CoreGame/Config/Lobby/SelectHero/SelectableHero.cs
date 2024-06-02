using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace AgeOfMatchic.Config.SelectHero
{
	public class SelectableHero : MonoBehaviour
	{
		public Button HideButton;
		private readonly List<HeroIcon> HeroBuffer = new();
		public HeroIcon Original;
		public RectTransform Target;

		public void Show(Action<HeroIcon> selectedHero)
		{
			gameObject.SetActive(true);
			HideButton.onClick.RemoveAllListeners();
			HideButton.onClick.AddListener(Hide);
			foreach (var hero in HeroBuffer)
			{
				Destroy(hero.gameObject);
			}
			
			HeroBuffer.Clear();
			var selectedHeroes = new List<string>(Profile.Instance.SelectedHeroes);
			var heroes = new List<string>(Profile.Instance.Heroes);
			heroes.RemoveAll(item => selectedHeroes.Contains(item));
			var config = GraphLobby.Instance.Config;

			for (var index = 0; index < heroes.Count; index++)
			{
				var hero = heroes[index];
				var selectable = Instantiate(Original, Target);
				selectable.Icon.sprite 
					= config.Config.Characters
						.ToList()
						.Find(x => x.CharacterId == hero).VisualData.Sprite;
				selectable.Button.onClick.RemoveAllListeners();
				selectable.Button.onClick.AddListener(() => selectedHero.Invoke(selectable));
				selectable.HeroId = hero;

				HeroBuffer.Add(selectable);
			}
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace AgeOfMatchic.Config.SelectHero
{
	public class SelectedHero : MonoBehaviour
	{
		public HeroIcon[] Heroes;
		public SelectableHero SelectableHero;
		private HeroIcon _hero;

		private void Start()
		{
			var selectedHeroes = Profile.Instance.SelectedHeroes;
			var config = GraphLobby.Instance.Config;

			for (var index = 0; index < selectedHeroes.Count; index++)
			{
				var selectedHero = selectedHeroes[index];
				var heroIcon = Heroes[index];
				heroIcon.Icon.sprite 
					= config.Config.Characters
						.ToList()
						.Find(x => x.CharacterId == selectedHero).VisualData.Sprite;
				heroIcon.Button.onClick.RemoveAllListeners();
				heroIcon.Button.onClick.AddListener(() => OnClickHero(heroIcon));
				heroIcon.HeroId = selectedHero;
			}
		}

		private void OnClickHero(HeroIcon hero)
		{
			_hero = hero;
			SelectableHero.Show(OnSelect);
		}

		private void OnSelect(HeroIcon obj)
		{
			var findIndex = Profile.Instance.SelectedHeroes.FindIndex(x => x == _hero.HeroId);
			Profile.Instance.SelectedHeroes[findIndex] = obj.HeroId;
			Start();
			SelectableHero.Hide();
			Profile.Instance.Save();
		}
	}
}
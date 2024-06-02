using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Common.Interfaces;
using Match3.Infrastructure;
using UnityEngine;
using Random = System.Random;

namespace Common
{
    public class UnityItemGenerator : ItemGenerator<IUnityItem>, IUnityItemGenerator
    {
        private readonly Random _random;
        private readonly Transform _container;
        private readonly GameObject _itemPrefab;

        private Sprite[] _sprites;

        public UnityItemGenerator(GameObject itemPrefab, Transform container)
        {
            _random = new Random();
            _container = container;
            _itemPrefab = itemPrefab;
        }

        public void SetSprites(Sprite[] sprites)
        {
            _sprites = sprites;
        }

        public string[] GetContentIds()
        {
            return _sprites.Select(x => x.name).ToArray();
        }

        protected override IUnityItem CreateItem()
        {
            var item = _itemPrefab.CreateNew<IUnityItem>(parent: _container);
            item.Hide();

            return item;
        }

        protected override IUnityItem ConfigureItem(IUnityItem item)
        {
            var strings = new List<string>{"Gold", "Silver"};
            item.SpriteRenderer.color = Color.white;
            strings.AddRange(BattleUi.Instance.AllyAbilities);
            var battleUi = BattleUi.Instance;
            var eventProbabilities = battleUi.Config.EventProbabilities;
            var index = _random.Next(0, 100);
            if (index < eventProbabilities.AbilitiesProbability)
            {
                var next = _random.Next(0, battleUi.AllyAbilities.Length);
                var battleUiAllyAbility = battleUi.AllyAbilities[next];
                item.SetSprite(strings.FindIndex(x => x.Equals(battleUiAllyAbility)), _sprites.ToList().Find(x => x.name == $"{battleUiAllyAbility}(Clone)"));
                return item;
            }
            if (index > eventProbabilities.AbilitiesProbability && index < 100 - eventProbabilities.GoldProbability)
            {
                item.SetSprite(strings.FindIndex(x => x.Equals("Silver")), _sprites.ToList().Find(x => x.name == "Silver(Clone)"));
                return item;
            }

            item.SetSprite(strings.FindIndex(x => x.Equals("Gold")), _sprites.ToList().Find(x => x.name == "Gold(Clone)"));
            return item;
        }
    }
}
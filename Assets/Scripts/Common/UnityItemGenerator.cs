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
            strings.AddRange(BattleUi.Instance.AllyAbilities);
            var index = _random.Next(0, strings.Count);
            var sprite = _sprites.ToList().Find(x => x.name == $"{strings[index]}(Clone)");
            item.SetSprite(index, sprite);

            return item;
        }
    }
}
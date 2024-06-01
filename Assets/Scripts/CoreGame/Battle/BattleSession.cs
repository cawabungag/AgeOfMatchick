using System.Linq;
using AgeOfMatchic.Config;
using UnityEngine;

namespace AgeOfMatchic.Battle
{
	public class BattleSession : IBattleSession
	{
		[SerializeField]
		private ConfigObject _config;
		
		private int AllyHealth;
		private int AllyShield;
		
		private int EnemyHealth;
		private int EnemySheild;
		
		public BattleSession(ConfigObject configObject)
		{
			
		}

		public void CreateSession(string[] allyHeroesId, string[] enemyHeroesId)
		{
			foreach (var ally in allyHeroesId)
			{
				var allyConfig = _config.Config.Characters.ToList().Find(x => x.CharacterId == ally);
				AllyHealth += allyConfig.ChatacterStatsConfig.Health;
				AllyShield += allyConfig.ChatacterStatsConfig.Shield;
			}
			
			foreach (var enemy in enemyHeroesId)
			{
				var enemyConfig = _config.Config.Characters.ToList().Find(x => x.CharacterId == enemy);
				EnemyHealth += enemyConfig.ChatacterStatsConfig.Health;
				EnemySheild += enemyConfig.ChatacterStatsConfig.Shield;
			}
		}
	}

	public interface IBattleSession
	{
	}
}
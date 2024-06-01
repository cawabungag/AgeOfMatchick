using UnityEngine;

namespace AgeOfMatchic.Config
{
	[CreateAssetMenu(menuName = "Config", fileName = "Config")]
	public class ConfigObject : ScriptableObject
	{
		[SerializeField]
		private AomConfig _config;
		public AomConfig Config => _config;

		public EventProbabilities EventProbabilities;
		public LevelsData LevelsData;
	}
}
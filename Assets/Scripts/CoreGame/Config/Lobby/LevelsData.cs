using UnityEngine;

namespace AgeOfMatchic.Config
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
	public class LevelsData : ScriptableObject
	{
		public LevelData[] Data;
	}
}
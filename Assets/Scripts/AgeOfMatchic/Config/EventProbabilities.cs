using UnityEngine;

namespace AgeOfMatchic.Config
{
	[CreateAssetMenu(fileName = "EventProbabilities", menuName = "ScriptableObjects/EventProbabilities", order = 1)]
	public class EventProbabilities : ScriptableObject
	{
		[Range(0, 100)]
		public float AbilitiesProbability;
		[Range(0, 100)]
		public float Silver;
		[Range(0, 100)]
		public float GoldProbability;
	}
}
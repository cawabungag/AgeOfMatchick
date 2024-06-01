using UnityEngine;

namespace AgeOfMatchic.Config
{
	[CreateAssetMenu(fileName = "EventProbabilities", menuName = "ScriptableObjects/EventProbabilities", order = 1)]
	public class EventProbabilities : ScriptableObject
	{
		[Range(0, 100)]
		public float event1Probability;
		[Range(0, 100)]
		public float event2Probability;
		[Range(0, 100)]
		public float event3Probability;

		private void OnValidate()
		{
			AdjustProbabilities();
		}

		private void AdjustProbabilities()
		{
			float total = event1Probability + event2Probability + event3Probability;
			if (total > 100)
			{
				float excess = total - 100;
				float totalWithoutMax = total - Mathf.Max(event1Probability, Mathf.Max(event2Probability, event3Probability));

				if (totalWithoutMax == 0)
				{
					// Если только одно значение максимальное, просто уменьшаем его на излишек
					if (event1Probability >= event2Probability && event1Probability >= event3Probability)
					{
						event1Probability -= excess;
					}
					else if (event2Probability >= event1Probability && event2Probability >= event3Probability)
					{
						event2Probability -= excess;
					}
					else
					{
						event3Probability -= excess;
					}
				}
				else
				{
					float scale = (total - excess) / totalWithoutMax;
					if (event1Probability >= event2Probability && event1Probability >= event3Probability)
					{
						event2Probability *= scale;
						event3Probability *= scale;
					}
					else if (event2Probability >= event1Probability && event2Probability >= event3Probability)
					{
						event1Probability *= scale;
						event3Probability *= scale;
					}
					else
					{
						event1Probability *= scale;
						event2Probability *= scale;
					}
				}
			}
		}
	}
}
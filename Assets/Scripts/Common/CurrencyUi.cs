using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Common
{
	public class CurrencyUi : MonoBehaviour
	{
		public float moveDuration = 2.0f;
		public float destroyDelay = 1.0f;
			
		private int _gold;
		private int _silver;
		
		[SerializeField]
		private TextMeshProUGUI _goldCount;

		[SerializeField]
		private TextMeshProUGUI _silverCount;
		
		[SerializeField]
		private RectTransform _goldTarget;

		[SerializeField]
		private RectTransform _silverTarget;
		
		[SerializeField]
		private GameObject _silverPrefab;
		
		[SerializeField]
		private GameObject _goldPrefab;

		[SerializeField]
		private Camera _uiCamera;
		public Material alwaysOnTopMaterial;

		
		
		public static CurrencyUi Instance;

		private void Awake()
		{
			Instance = this;
			UpdateUi();
		}

		public void IncreaseGold(int delta, Vector3[] pos)
		{
			_gold += delta;

			var seq = DOTween.Sequence();
			foreach (var p in pos)
			{
				var spawnedObject = Instantiate(_goldPrefab, p, Quaternion.identity);
				var worldTargetPosition = _uiCamera.ScreenToWorldPoint(_goldTarget.position);
				worldTargetPosition.z = 0;

				var seqloc = spawnedObject.transform.DOMove(worldTargetPosition, moveDuration)
					.OnComplete(() =>
					{
						Destroy(spawnedObject, destroyDelay);
					});
				seq.Append(seqloc);
			}

			seq.OnComplete(UpdateUi);
		}
		
		public void IncreaseSilver(int delta, Vector3[] pos)
		{
			_silver += delta;
			
			var seq = DOTween.Sequence();
			foreach (var p in pos)
			{
				var spawnedObject = Instantiate(_silverPrefab, p, Quaternion.identity);
				var worldTargetPosition = _uiCamera.ScreenToWorldPoint(_silverTarget.position);
				worldTargetPosition.z = 0;

				var seqloc = spawnedObject.transform.DOMove(worldTargetPosition, moveDuration)
					.OnComplete(() =>
					{
						Destroy(spawnedObject, destroyDelay);
					});
				seq.Append(seqloc);
			}

			seq.OnComplete(UpdateUi);
		}

		private void UpdateUi()
		{
			_goldCount.text = _gold.ToString();
			_silverCount.text = _silver.ToString();
		}
	}
}
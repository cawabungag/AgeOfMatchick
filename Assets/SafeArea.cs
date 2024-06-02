using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
	private RectTransform panelSafeArea;
	private Rect lastSafeArea = new(0, 0, 0, 0);
	private ScreenOrientation lastOrientation = ScreenOrientation.Portrait;
	private Vector2Int lastResolution = new(0, 0);

	void Awake()
	{
		panelSafeArea = GetComponent<RectTransform>();
		ApplySafeArea();
	}

	void Update()
	{
		if (lastOrientation != Screen.orientation || lastResolution.x != Screen.width || lastResolution.y != Screen.height)
		{
			ApplySafeArea();
		}
	}

	void ApplySafeArea()
	{
		Rect safeArea = Screen.safeArea;

		if (safeArea != lastSafeArea)
		{
			lastSafeArea = safeArea;

			// Convert safe area rectangle from absolute pixels to normalized anchor coordinates
			Vector2 anchorMin = safeArea.position;
			Vector2 anchorMax = safeArea.position + safeArea.size;
			anchorMin.x /= Screen.width;
			anchorMin.y /= Screen.height;
			anchorMax.x /= Screen.width;
			anchorMax.y /= Screen.height;

			panelSafeArea.anchorMin = anchorMin;
			panelSafeArea.anchorMax = anchorMax;
		}

		lastOrientation = Screen.orientation;
		lastResolution.x = Screen.width;
		lastResolution.y = Screen.height;
	}
}
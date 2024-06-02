using UnityEngine;

public class AdaptImageSize : MonoBehaviour
{
	// Desired width and height to fit the sprite within
	public float targetWidth = 300;
	public float targetHeight = 300;

	private SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer == null)
		{
			Debug.LogError("AdaptImageSize script requires a SpriteRenderer component.");
			return;
		}

		AdjustSpriteSize();
	}

	void AdjustSpriteSize()
	{
		Sprite sprite = spriteRenderer.sprite;
		if (sprite == null)
		{
			Debug.LogError("SpriteRenderer does not have a sprite assigned.");
			return;
		}

		// Get the size of the sprite
		float spriteWidth = sprite.bounds.size.x;
		float spriteHeight = sprite.bounds.size.y;

		// Calculate the aspect ratios
		float targetAspect = targetWidth / targetHeight;
		float spriteAspect = spriteWidth / spriteHeight;

		// Determine the scale factor to fit the sprite within the target dimensions
		float scaleFactor = 1.0f;
		if (spriteAspect > targetAspect)
		{
			// Sprite is wider than the target area, scale based on width
			scaleFactor = targetWidth / spriteWidth;
		}
		else
		{
			// Sprite is taller than the target area, scale based on height
			scaleFactor = targetHeight / spriteHeight;
		}

		// Apply the scale factor
		transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);
	}
}
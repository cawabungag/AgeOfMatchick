using UnityEngine;

public class GridScaler : MonoBehaviour
{
	public Vector2 baseResolution = new Vector2(1920, 1080); // Базовое разрешение, на которое вы ориентируетесь
	public Vector3 baseScale = new Vector3(1, 1, 1); // Базовый масштаб для базового разрешения

	void Start()
	{
		AdjustScale();
	}

	void AdjustScale()
	{
		// Получение текущего разрешения экрана
		float currentWidth = Screen.width;
		float currentHeight = Screen.height;

		// Вычисление коэффициентов изменения по ширине и высоте
		float widthRatio = currentWidth / baseResolution.x;
		float heightRatio = currentHeight / baseResolution.y;

		// Выбор минимального коэффициента для пропорционального изменения масштаба
		float scaleRatio = Mathf.Min(widthRatio, heightRatio);

		// Применение пропорционального масштабирования
		transform.localScale = new Vector3(baseScale.x * scaleRatio, baseScale.y * scaleRatio, baseScale.z * scaleRatio);
	}
}
using UnityEngine;
using UnityEngine.EventSystems;

public class InputView : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
	{
		RaycastHit2D hitInfo = new RaycastHit2D();
		var screenPointToRay = Camera.main.ScreenToWorldPoint(eventData.position);
		hitInfo = Physics2D.Raycast(screenPointToRay, Vector2.zero);
		if (hitInfo.transform != null)
		{
			var component = hitInfo.transform.GetComponent<Node>();
			if (hitInfo.collider != null && component)
			{
				GraphLobby.Instance.OnSelectNode(component);
				Debug.Log("Hit object: " + hitInfo.collider.gameObject.name);
			}
		}
	}
}
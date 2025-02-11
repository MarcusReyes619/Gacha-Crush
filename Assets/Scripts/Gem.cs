using UnityEngine;
using UnityEngine.UI;

public enum GemType { Blue, Green, Orange, Purple, Red, Teal }

public class Gem : MonoBehaviour
{
	[SerializeField] public GemType gemType;
	[SerializeField] private SpriteRenderer sprite;
	private GridManager gridManager;

	private void Start()
	{
		var gridObject = GameObject.Find("GridManager");
		gridManager = gridObject.GetComponent<GridManager>();
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			gridManager.SelectObject((int)transform.position.x, (int)transform.position.y);
		}
	}
}

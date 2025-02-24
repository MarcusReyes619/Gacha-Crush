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

	public void SelectObject(int posX, int posY)
	{
		if (gridManager.selectedObject == null)
		{
			gridManager.SelectObject((int)transform.localPosition.x, (int)transform.localPosition.y);
		}
		else
		{
			gridManager.ChooseSwapGems(posX, posY);
		}
	}
}

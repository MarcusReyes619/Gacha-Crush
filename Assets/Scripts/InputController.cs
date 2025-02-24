using UnityEngine;

public class InputController : MonoBehaviour
{
	[SerializeField] private Camera currentCamera;

	private Vector2 startPosition;
	private Vector2 endPosition;
	private GridManager gridManager;

	private Gem selectedGem;

	private void Start()
	{
		var gridObject = GameObject.Find("GridManager");
		gridManager = gridObject.GetComponent<GridManager>();
	}

	private void Update()
	{
		if (Input.touchCount > 0 && !GameManager.instance.IsGamePaused)
		{
			GetGemFromTouchPosition();
		}
    }

	private void GetGemFromMousePosition()
	{
		RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
		Debug.Log(rayHit.transform.name);

		selectedGem = rayHit.transform.GetComponent<Gem>();
		selectedGem.SelectObject(0,0);

		startPosition = GetMousePosition();
	}

	private void GetGemFromTouchPosition()
	{
		Touch touch = Input.GetTouch(0);

		if (touch.phase == TouchPhase.Began)
		{
			Vector3 touchPosition = new Vector3(touch.position.x, touch.position.y);

			RaycastHit2D rayHit = Physics2D.GetRayIntersection(currentCamera.ScreenPointToRay(touchPosition));
			//Debug.Log(rayHit.transform.name);

			selectedGem = rayHit.transform.GetComponent<Gem>();
			selectedGem.SelectObject(0, 0);

			startPosition = GetMousePosition();
		}
		else if (touch.phase == TouchPhase.Ended)
		{
			endPosition = currentCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
			
			CalculateSwapGem();
		}
	}

	private void CalculateSwapGem()
	{
		float compareX = endPosition.x - startPosition.x;
		float compareY = endPosition.y - startPosition.y;
		float absoluteX = 0;
		float absoluteY = 0;

		if (compareX < 0) absoluteX = -compareX;
		else absoluteX = compareX;
		if (compareY < 0) absoluteY = -compareY;
		else absoluteY = compareY;

		float checkForMinDistance = Mathf.Max(absoluteX, absoluteY);
		if (checkForMinDistance < gridManager.mininumSwapDistance) // if touch movement is too small, don't swap and reset
		{
			gridManager.DeselectObject();
			return;
		}

		if (absoluteX > absoluteY)
		{
			if (compareX > 0) selectedGem.SelectObject(1,0);
			else selectedGem.SelectObject(-1,0);
		}
		else
		{
			if (compareY > 0) selectedGem.SelectObject(0, 1);
			else selectedGem.SelectObject(0, -1);
		}

		gridManager.DeselectObject();
	}

    private Vector3 GetMousePosition()
    {
		Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = currentCamera.nearClipPlane + currentCamera.nearClipPlane * 0.01f;
		Vector3 worldPosition = currentCamera.ScreenToWorldPoint(mousePosition);

		return worldPosition;
	}
}

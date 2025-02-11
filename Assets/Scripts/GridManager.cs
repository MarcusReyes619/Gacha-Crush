using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int spacing = 1;
    [SerializeField] private int width, height;
    [SerializeField] private Gem[] gemPrefabs;
	[SerializeField] private Transform cameraTransform;
	private Gem selectedObject;

    Gem[,] grid;

    void Start()
    {
        GenerateGrid();
		SetCameraTransform();
    }

	private void SetCameraTransform() // center camera
	{
		cameraTransform.position = new Vector3(width / 2, height / 2, -10);
	}

    private void GenerateGrid() // width x height
    {
		grid = new Gem[width, height];

        for (int x = 0; x < width; x++)
        {
			for (int y = 0; y < height; y++)
			{
                GenerateGem(x, y);
			}
		}
    }

	private void GenerateGem(int x, int y)
	{
		GemType gemType = (GemType)Random.Range(0, 6);

		switch (gemType)
		{
			case GemType.Blue:
				grid[x, y] = Instantiate(gemPrefabs[0], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Green:
				grid[x, y] = Instantiate(gemPrefabs[1], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Orange:
				grid[x, y] = Instantiate(gemPrefabs[2], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Purple:
				grid[x, y] = Instantiate(gemPrefabs[3], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Red:
				grid[x, y] = Instantiate(gemPrefabs[4], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Teal:
				grid[x, y] = Instantiate(gemPrefabs[5], new Vector3(x, y), Quaternion.identity);
				break;
		}
	}

	public void SelectObject(int posX, int posY)
	{
		if (selectedObject != null)
		{
			var swapObject = grid[posX, posY];
			var selectedPos = selectedObject.transform.position;

			grid[posX, posY] = selectedObject;
			grid[posX, posY].transform.position = new Vector3(posX, posY, grid[posX, posY].transform.position.z);

			grid[(int)selectedPos.x, (int)selectedPos.y] = swapObject;
			grid[(int)selectedPos.x, (int)selectedPos.y].transform.position = selectedPos;

			selectedObject = null; //reset currently selected after

			CheckMatch(posX, posY);
			//CheckMatch((int)selectedPos.x, (int)selectedPos.y);
		}
		else
		{
			selectedObject = grid[posX, posY];
			print("selected object: " + selectedObject.name);
		}
	}

	private void CheckMatch(int x, int y)
	{
		int matchAmount = 1;
		int incrementAmount = 1;
		List<Vector2> coordinates = new List<Vector2>();

		GemType checkGemType = grid[x, y].gemType;

		while (true) // check left
		{
			if (x - incrementAmount < 0) break;

			if (grid[x - incrementAmount, y].gemType == checkGemType)
			{
				matchAmount += 1;
				incrementAmount += 1;
			}
			else break;
		}

		incrementAmount = 1;
		while (true) // check right
		{
			if (x + incrementAmount == width) break;

			if (grid[x + incrementAmount, y].gemType == checkGemType)
			{
				matchAmount += 1;
				incrementAmount += 1;
			}
			else break;
		}

		if (matchAmount >= 3)
		{
			print("match made!!!!");
			
		}
	}

}

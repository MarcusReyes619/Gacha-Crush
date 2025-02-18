using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int spacing = 1;
    [SerializeField] private int width, height;
    [SerializeField] private GameObject[] gemPrefabs;
	[SerializeField] private Transform cameraTransform;
	[SerializeField] private float addTime;
	private GameObject selectedObject;

	public float timer;

	[Header("UI")]
	[SerializeField] private Slider TimerSlider_UI;

    GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
		SetCameraTransform();
    }

	private void Update()
	{
		UpdateTimer();
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		TimerSlider_UI.value = timer;
	}

	private void SetCameraTransform() // center camera
	{
		cameraTransform.position = new Vector3(width / 2, height / 2, -10);
	}

    private void GenerateGrid() // width x height
    {
		grid = new GameObject[width, height];

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
			CheckMatch((int)selectedPos.x, (int)selectedPos.y);
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
		List<Vector2Int> coordinates = new List<Vector2Int>();
		
		Gem getGemClass = grid[x, y].GetComponent<Gem>();
		GemType checkGemType = getGemClass.gemType;

		coordinates.Add(new Vector2Int(x, y));

		while (true) // check left
		{
			if (x - incrementAmount < 0) break;

			if (grid[x - incrementAmount, y])
			{
				getGemClass = grid[x - incrementAmount, y].GetComponent<Gem>();
			}
			if (getGemClass.gemType == checkGemType)
			{
				matchAmount += 1;
				coordinates.Add(new Vector2Int(x - incrementAmount, y));

				incrementAmount += 1;
			}
			else break;
		}

		incrementAmount = 1;
		while (true) // check right
		{
			if (x + incrementAmount == width) break;

			if (grid[x + incrementAmount, y])
			{
				getGemClass = grid[x + incrementAmount, y].GetComponent<Gem>();
			}
			if (getGemClass.gemType == checkGemType)
			{
				matchAmount += 1;
				coordinates.Add(new Vector2Int(x + incrementAmount, y));

				incrementAmount += 1;
			}
			else break;
		}

		if (matchAmount >= 3)
		{
			MatchMade(coordinates);
		}
		matchAmount = 1;

		incrementAmount = 1;
		while (true) // check down
		{
			if (y - incrementAmount < 0) break;

			if (grid[x, y - incrementAmount])
			{
				getGemClass = grid[x, y - incrementAmount].GetComponent<Gem>();
			}
			if (getGemClass.gemType == checkGemType)
			{
				matchAmount += 1;
				coordinates.Add(new Vector2Int(x, y - incrementAmount));

				incrementAmount += 1;
			}
			else break;
		}

		incrementAmount = 1;
		while (true) // check up
		{
			if (y + incrementAmount == height) break;

			if (grid[x, y + incrementAmount])
			{
				getGemClass = grid[x, y + incrementAmount].GetComponent<Gem>();
			}
			if (getGemClass.gemType == checkGemType)
			{
				matchAmount += 1;
				coordinates.Add(new Vector2Int(x, y + incrementAmount));

				incrementAmount += 1;
			}
			else break;
		}

		if (matchAmount >= 3)
		{
			MatchMade(coordinates);
		}
	}

	private void MatchMade(List<Vector2Int> coordinates)
	{
		print("match made!!!");
		timer += addTime;
		DeleteMatchedGems(coordinates);
	}

	private void DeleteMatchedGems(List<Vector2Int> coordinates)
	{
		foreach (Vector2Int coordinate in coordinates)
		{
			Destroy(grid[coordinate.x, coordinate.y]); //play destroy animation here which calls drop higher gems
			grid[coordinate.x, coordinate.y] = null;
		}

		DropHigherGems(coordinates);
	}

	private void DropHigherGems(List<Vector2Int> coordinates)
	{
		foreach (Vector2Int coordinate in coordinates)
		{
			for (int y = coordinate.y; y < height; y++)
			{
				if (grid[coordinate.x, y] == null) continue;

				if (grid[coordinate.x, y]) SendDownGem(coordinate);
			}
		}
	}

	private void SendDownGem(Vector2Int coordinate)
	{
		if (grid[coordinate.x, coordinate.y - 1] == null)
		{
			grid[coordinate.x, coordinate.y - 1] = grid[coordinate.x, coordinate.y];
		}
	}
}

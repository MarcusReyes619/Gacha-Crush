using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float spacingScale = 1;
	[SerializeField] private float imageScale = 1;
	[SerializeField] private int width, height;
	[SerializeField] private Transform cameraTransform;
	[SerializeField] private Transform gemHolderTransform;
	[SerializeField] private GameObject[] gemPrefabs;
	[SerializeField] private float addTime;
	[SerializeField] private float mininumSwapDistance;
	public GameObject selectedObject;

	public float timer;

	[Header("UI")]
	[SerializeField] private Slider TimerSlider_UI;
	[SerializeField] private TMP_Text scoreText;
	[SerializeField] private ScoreData scoreData;

	private int scoreMult;
    GameObject[,] grid;

    void Start()
    {
		SetGemHolderTransform();
		GenerateGrid();
		scoreData.Subscribe(UpdateScoreUI);
		GameManager.instance.IsGamePaused = false;
    }

	private void Update()
	{
		if (!GameManager.instance.IsGamePaused)
		{
			UpdateTimer();
		}
	}

	private void UpdateTimer()
	{
		timer -= Time.deltaTime;
		TimerSlider_UI.value = timer;
	}

	private void SetGemHolderTransform() // scale objects
	{
		gemHolderTransform.localScale = new Vector3(1 * spacingScale, 1 * spacingScale);
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
				grid[x, y] = Instantiate(gemPrefabs[0], gemHolderTransform);
				break;
			case GemType.Green:
				grid[x, y] = Instantiate(gemPrefabs[1], gemHolderTransform);
				break;
			case GemType.Orange:
				grid[x, y] = Instantiate(gemPrefabs[2], gemHolderTransform);
				break;
			case GemType.Purple:
				grid[x, y] = Instantiate(gemPrefabs[3], gemHolderTransform);
				break;
			case GemType.Red:
				grid[x, y] = Instantiate(gemPrefabs[4], gemHolderTransform);
				break;
			case GemType.Teal:
				grid[x, y] = Instantiate(gemPrefabs[5], gemHolderTransform);
				break;
		}
		grid[x, y].transform.localPosition = new Vector3(x, y);
		grid[x, y].transform.localScale = new Vector3(1 * imageScale, 1 * imageScale);
	}


	public void SelectObject(int posX, int posY)
	{
		selectedObject = grid[posX, posY];
	}

	public void DeselectObject()
	{
		selectedObject = null;
	}

	public void ChooseSwapGems(int posX, int posY)
	{
		var selectedPos = selectedObject.transform.localPosition;
		SwapObjects((int)selectedPos.x + posX, (int)selectedPos.y + posY);
	}

	public void SwapObjects(int posX, int posY)
	{
		if (selectedObject == null) return;

		var swapObject = grid[posX, posY];
		var selectedPos = selectedObject.transform.localPosition;

		grid[posX, posY] = selectedObject;
		grid[posX, posY].transform.localPosition = new Vector3(posX, posY, grid[posX, posY].transform.localPosition.z);

		grid[(int)selectedPos.x, (int)selectedPos.y] = swapObject;
		grid[(int)selectedPos.x, (int)selectedPos.y].transform.localPosition = selectedPos;

		selectedObject = null; //reset currently selected after

		List<Vector2Int> firstMatchCoordinates = CheckMatch(posX, posY);
		firstMatchCoordinates.AddRange(CheckMatch((int)selectedPos.x, (int)selectedPos.y));

		DropHigherGems(firstMatchCoordinates);

		SimpleFillEmptyGems();

	}

	private List<Vector2Int> CheckMatch(int x, int y)
	{
		int matchAmount = 1;
		int incrementAmount = 1;
		List<Vector2Int> coordinates = new List<Vector2Int>();
		List<Vector2Int> matchCoordinates = new List<Vector2Int>();

		Gem getGemClass;
		GemType checkGemType;

		if (grid[x, y] == null)
		{
			return matchCoordinates;
		}
		else
		{
			getGemClass = grid[x, y].GetComponent<Gem>();
			checkGemType = getGemClass.gemType;
		}

		
		

		coordinates.Add(new Vector2Int(x, y));

		while (true) // check left
		{
			if (x - incrementAmount < 0) break;

			if (grid[x - incrementAmount, y] != null)
			{
				getGemClass = grid[x - incrementAmount, y].GetComponent<Gem>();
			}
			else break;

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

			if (grid[x + incrementAmount, y] != null)
			{
				getGemClass = grid[x + incrementAmount, y].GetComponent<Gem>();
			}
			else break;

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
            scoreMult = matchAmount - 3;
            matchCoordinates.AddRange(MatchMade(coordinates));
		}

		// Clear values for up/down check
		matchAmount = 1; 
		coordinates.Clear();
		coordinates.Add(new Vector2Int(x, y));

		incrementAmount = 1;
		while (true) // check down
		{
			if (y - incrementAmount < 0) break;

			if (grid[x, y - incrementAmount] != null)
			{
				getGemClass = grid[x, y - incrementAmount].GetComponent<Gem>();
			}
			else break;

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

			if (grid[x, y + incrementAmount] != null)
			{
				getGemClass = grid[x, y + incrementAmount].GetComponent<Gem>();
			}
			else break;

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
			scoreMult = matchAmount - 3;
			matchCoordinates.AddRange(MatchMade(coordinates));
		}

		return matchCoordinates;
	}

	private List<Vector2Int> MatchMade(List<Vector2Int> coordinates)
	{
		print("match made!!!");
		scoreData.Add(scoreMult > 0 ? 100 * scoreMult : 100);
		timer += addTime;
		return DeleteMatchedGems(coordinates);
	}

    private void UpdateScoreUI(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }

    private List<Vector2Int> DeleteMatchedGems(List<Vector2Int> coordinates)
	{
		foreach (Vector2Int coordinate in coordinates)
		{
			Destroy(grid[coordinate.x, coordinate.y]); //play destroy animation here which calls drop higher gems
			grid[coordinate.x, coordinate.y] = null;
		}

		return coordinates;
	}

	private void DropHigherGems(List<Vector2Int> coordinates)
	{
		foreach (Vector2Int coordinate in coordinates)
		{
			for (int y = coordinate.y; y < height; y++)
			{
				if (grid[coordinate.x, y] == null) continue;

				if (grid[coordinate.x, y]) SendDownGem(new Vector2Int(coordinate.x, y));
			}
		}
	}

	private void SendDownGem(Vector2Int coordinate)
	{
		for (int y = coordinate.y; y > 0; y--) //stop when selecting the bottom slot
		{
			if (grid[coordinate.x, y - 1] == null)
			{
				// set lower null space equal to current higher gem and update position accordingly
				grid[coordinate.x, y - 1] = grid[coordinate.x, y];
				grid[coordinate.x, y - 1].transform.localPosition = new Vector3(coordinate.x, y - 1);

				// set null as the current higher gem has been moved down
				grid[coordinate.x, y] = null;
			}
			else return;
		}
	}

	private void SimpleFillEmptyGems()
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				if (grid[x,y] == null)
				{
					GenerateGem(x, y);
				}
			}
		}
	}
}

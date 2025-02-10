using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] int spacing = 1;
    [SerializeField] int width, height;
    [SerializeField] Gem[] gemPrefabs;

    GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid() // 8x8 
    {
        for (int x = 0; x < width; x++)
        {
			for (int y = 0; y < height; y++)
			{
                SetColor(x, y);
			}
		}
    }

	public void SetColor(int x, int y)
	{
		GemType gemType = (GemType)Random.Range(1, 6);

		switch (gemType)
		{
			case GemType.Blue:
				var gemObject = Instantiate(gemPrefabs[0], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Green:
				gemObject = Instantiate(gemPrefabs[1], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Orange:
				gemObject = Instantiate(gemPrefabs[2], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Purple:
				gemObject = Instantiate(gemPrefabs[3], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Red:
				gemObject = Instantiate(gemPrefabs[4], new Vector3(x, y), Quaternion.identity);
				break;
			case GemType.Teal:
				gemObject = Instantiate(gemPrefabs[5], new Vector3(x, y), Quaternion.identity);
				break;
		}
	}
}

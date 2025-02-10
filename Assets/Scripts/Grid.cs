using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] int spacing = 1;
    [SerializeField] int width, height;
    [SerializeField] Gem gemPrefab;

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
                var gemObject = Instantiate(gemPrefab, new Vector3(x,y), Quaternion.identity);
			}
		}
    }
}

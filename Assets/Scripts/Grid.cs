using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] int spacing = 5;
    [SerializeField] GameObject gemObject;

    Gem[,] grid;

    void Start()
    {
        GenerateGrid();
        RenderGems();
    }

    void Update()
    {
        
    }

    void GenerateGrid() // 8x8 
    {
        for (int x = 0; x < 8; x++)
        {
			for (int y = 0; y < 8; y++)
			{
				grid[x, y] = new Gem();
			}
		}
    }

    void RenderGems()
    {
		for (int x = 0; x < 8; x++)
		{
			for (int y = 0; y < 8; y++)
			{
				Instantiate(grid[x, y]);
			}
		}
	}
}

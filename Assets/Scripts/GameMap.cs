using UnityEditor.UI;
using UnityEngine;

public class GameMap : MonoBehaviour
{
    [SerializeField] private Tile groundPrefab;

    private Tile[] tiles;


    public void Initialize(Vector2Int size) 
    {
        tiles = new Tile[size.x * size.y];
        for (int i = 0, x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++, i++)
            {
                Tile current = tiles[i] = Instantiate(groundPrefab);
                current.transform.SetParent(transform);
                current.transform.localPosition = new Vector3(x, 0f, y);

                if (y > 0)
                {
                    tiles[i].southNeighbour = tiles[i - 1];
                    tiles[i - 1].northNeighbour = tiles[i];
                }
                if (x > 0)
                {
                    tiles[i].westNeighbour = tiles[i - size.x];
                    tiles[i - size.x].eastNeighbour = tiles[i];
                }
            }
        }
        
    }
}
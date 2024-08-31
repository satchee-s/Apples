using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    GameObject[] tilesInLevel;
    List<Sprite> tileImages = new List<Sprite>();
    float currentPositionX, currentPositionY;

    private void Start()
    {
        object[] loadedImages = Resources.LoadAll("TileImages", typeof(Sprite));
        for (int i = 0; i < loadedImages.Length; i++)
            tileImages.Add((Sprite)loadedImages[i]);
    }

    void ShuffleTiles()
    {
        int[] index = new int[tileImages.Count];
        for (int i = 0; i < index.Length; i++)
            index[i] = i;
        for (int j = 0; j < index.Length; j++)
        {
            int k = Random.Range(j, index.Length);
            int temp = index[j];
            index[j] = index[k];
            index[k] = temp;
        }
    }

    public void CreateTileLayout(int gridSize)
    {
        tilesInLevel = new GameObject[gridSize * 4];
        for (int z = 0; z < gridSize; z++)
        {
            for (int x = 0; x < 4; x++)
            {
                int i = x + z * (gridSize - 1);
                Vector3 position = transform.position + new Vector3(x * 1.5f, z * 1.5f, 0f);
                tilesInLevel[i] = Instantiate(tilePrefab, position, Quaternion.identity);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _tilesPrefab;
    [SerializeField]
    private List<Color> _colors;
    private GameObject[,] _tiles;

    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale = 10;
    [SerializeField] private Vector2 offset;

    private void Start()
    {
        _tiles = new GameObject[width, height];

        Generate();
        UpdateTerrain(_tiles);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateTerrain(_tiles);
    }

    private void Generate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _tiles[x, y] = Instantiate(_tilesPrefab[0], new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    private void UpdateTerrain(GameObject[,] _tiles)
    {
        if (_tiles != null)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _tiles[x, y].GetComponent<SpriteRenderer>().color = _colors[GetTilesId(x, y)];
            }
        }
    }

    private int GetTilesId(int x, int y)
    {

        float xCoord = (float)x / width * scale + offset.x;
        float yCoord = (float)y / height * scale + offset.y;

        float perlinNoise = Mathf.Clamp(Mathf.PerlinNoise(xCoord, yCoord), 0, 1);

        int finalNoise;
        if (perlinNoise < 0.1f)
            finalNoise = 0;
        else if (perlinNoise < 0.2f)
            finalNoise = 1;
        else if (perlinNoise < 0.3f)
            finalNoise = 2;
        else if (perlinNoise < 0.5f)
            finalNoise = 3;
        else if (perlinNoise < 0.6f)
            finalNoise = 4;
        else if (perlinNoise < 0.8f)
            finalNoise = 5;
        else
            finalNoise = 6;

        return finalNoise;
    }
}

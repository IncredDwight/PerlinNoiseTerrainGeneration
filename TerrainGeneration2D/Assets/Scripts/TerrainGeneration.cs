using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TerrainGeneration : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _tiles;

    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale = 10;
    [SerializeField] private Vector2 offset;

    private void Start()
    {
        Generate();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Generate()
    {
        offset.x = Random.Range(-100, 100);
        offset.y = Random.Range(-100, 100);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Instantiate(_tiles[GetTilesId(x, y)], new Vector2(x, y), Quaternion.identity);
            }
        }
    }

    private int GetTilesId(int x, int y)
    {

        float xCoord = (float)x / width * scale + offset.x;
        float yCoord = (float)y / height * scale + offset.y;

        float perlinNoise = Mathf.PerlinNoise(xCoord, yCoord);

        int finalNoise; //= (perlinNoise != 1) ? Mathf.FloorToInt(perlinNoise * _tiles.Count) : _tiles.Count - 1;
        if (perlinNoise < 0.3f)
            finalNoise = 0;
        else if (perlinNoise < 0.5f)
            finalNoise = 1;
        else if (perlinNoise < 0.7f)
            finalNoise = 2;
        else
            finalNoise = 3;

        return finalNoise;
    }
}

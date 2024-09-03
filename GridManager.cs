using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridManager : MonoBehaviour
{

    // Serialized field to choose the environment type from a dropdown in the Inspector
    [SerializeField] private EnvironmentType[] possibleEnvironments;
    [SerializeField] private EnvironmentType selectedEnvironmentType;

    [SerializeField] private EventType[] possibleEventType;
    [SerializeField] private EventType selectedEventType;

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    [SerializeField] private Transform _cam;
    [SerializeField] private Camera _mainCamera;
    private Vector3 _previousCameraPosition; // Store previous camera position

    private PerlinNoiseValue _pnv;
    [SerializeField] private float[] environmentThresholds; // Array to store the threshold variances
    // Variable to control the variance value for threshold calculation
    [SerializeField] private float varianceValue = 0.05f; // Adjust as needed

    //ChunkManagerValues
    ChunkManager chunkManager;
    [SerializeField] private int _chunkSize;
    private List<Tile> _tiles = new List<Tile>();


    private void Start()
    {

        _mainCamera = Camera.main;
        _cam = _mainCamera.transform;
        _pnv = GetComponent<PerlinNoiseValue>();
        _previousCameraPosition = _cam.position;
        
        // Initialize the environment thresholds before generating tiles
        InitializeThresholds(varianceValue);

        GenerateGrid();

        // Add ChunkManager as a component to this GameObject
        chunkManager = gameObject.AddComponent<ChunkManager>();

        // Create an instance of ChunkManager and pass the necessary data
        chunkManager.Initialize(_chunkSize, _tiles, _width, _height);
        chunkManager.CreateChunks(); // Call to create and initialize chunks
    }
    void GenerateGrid()
    {
        for (int y = 0; y < _height; y++) 
        {
            for (int x = 0; x < _width; x++)
            {
                //Instatiate the Tile Prefab at its coord
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                
                //Name the prefab based on its coord.
                spawnedTile.name = $"Tile {x} {y}";

                //Calculate the Perlin Value based on coord.
                float tilePerlinValue = _pnv.CalculatePerlinValue(x, y);
                

                // Get the environment type based on Perlin noise
                selectedEnvironmentType = GetEnvironmentType(tilePerlinValue);

                // Initialize each tile with its environment type determined by Perlin noise
                spawnedTile.Init(tilePerlinValue, selectedEnvironmentType);

                _tiles.Add(spawnedTile);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
    }

    public EnvironmentType GetEnvironmentType(float perlinValue)
    {
        for (int i = 0; i < environmentThresholds.Length; i++)
        {
            if (perlinValue <= environmentThresholds[i])
            {
                return possibleEnvironments[i];
            }
        }

        return possibleEnvironments[possibleEnvironments.Length - 1]; // Default return type
    }

    // Method to initialize and calculate thresholds
    private void InitializeThresholds(float varianceValue)
    {
        int environmentCount = possibleEnvironments.Length;
        environmentThresholds = new float[environmentCount];
        float stepSize = 1f / environmentCount;

        float lastValue = 0f;

        for (int i = 0; i < environmentCount; i++)
        {
            if (i == 0)
            {
                // First item with variance
                environmentThresholds[i] = Random.Range(stepSize - varianceValue, stepSize + varianceValue);
            }
            else
            {
                // Calculate remaining items
                environmentThresholds[i] = lastValue + Random.Range(stepSize - varianceValue, stepSize + varianceValue);

                // Ensure we do not exceed 1
                if (environmentThresholds[i] > 1f)
                {
                    environmentThresholds[i] = 1f;
                    break;
                }
            }

            lastValue = environmentThresholds[i];
        }
    }


}

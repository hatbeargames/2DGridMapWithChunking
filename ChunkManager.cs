using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapEnums;
public class ChunkManager : MonoBehaviour
{
    [SerializeField]private int _chunkSize; // Size of each chunk
    private List<Tile> _tiles; // List of all tiles in the grid
    [SerializeField]private List<MapChunk> _chunks; // List to store created chunks
    [SerializeField]private int _width; // Width of the grid
    [SerializeField] private int _height; // Height of the grid

    public void Initialize(int chunkSize, List<Tile> tiles, int width, int height)
    {
        _chunkSize = chunkSize;
        _tiles = tiles;
        _width = width;
        _height = height;
        _chunks = new List<MapChunk>();
    }

    // Method to create chunks from the list of tiles
    public void CreateChunks()
    {
        int numberOfChunksX = Mathf.CeilToInt((float)_width / _chunkSize);
        int numberOfChunksY = Mathf.CeilToInt((float)_height / _chunkSize);
        Debug.Log($"X: {numberOfChunksX}, Y: {numberOfChunksY}");
        for (int x = 0; x < numberOfChunksX; x++)
        {
            for (int y = 0; y < numberOfChunksY; y++)
            {
                Debug.Log($"Getting tiles for chunk {x},{y}");
                List<Tile> chunkTiles = GetTilesForChunk(x, y);
                MapChunk newChunk = CreateChunk(chunkTiles, x, y);
                _chunks.Add(newChunk);
            }
        }
    }
    // Method to retrieve tiles for a specific chunk based on chunk coordinates
    private List<Tile> GetTilesForChunk(int chunkX, int chunkY)
    {
        List<Tile> chunkTiles = new List<Tile>();

        int startX = chunkX * _chunkSize;
        int startY = chunkY * _chunkSize;
        Debug.Log($"StartX: {startX}, StartY {startY}");

        for (int y = startY; y < Mathf.Min(startY + _chunkSize, _height); y++) 
        {
            for (int x = startX; x < Mathf.Min(startX + _chunkSize, _width); x++)
            {
                Debug.Log($"X: {x}, Y: {y};");
                int tileIndex = y * _width + x;
                // Correct index calculation for 2D grid
                if (tileIndex >= 0 && tileIndex < _tiles.Count)
                    {
                        Debug.Log($"X is less than width and y less than height. tileIndex {tileIndex}");
                        chunkTiles.Add(_tiles[tileIndex]);
                    }
            }
        }

        return chunkTiles;
    }

    // Method to create a new chunk with a list of tiles
    private MapChunk CreateChunk(List<Tile> chunkTiles, int chunkX, int chunkY)
    {
        GameObject chunkObject = new GameObject($"Chunk_{chunkX}_{chunkY}");
        MapChunk newChunk = chunkObject.AddComponent<MapChunk>();
        newChunk.Init(_chunkSize, $"Chunk_{chunkX}_{chunkY}",chunkTiles);

        return newChunk;
    }
}

using System.Collections.Generic;
using UnityEngine;
using MapEnums;
public class MapChunk : MonoBehaviour
{
    [SerializeField] private List<Tile> _tiles; // List to store tiles within this chunk
    public string _mapChunkName; // The name for this specific chunk blueprint
    public Sprite _mapChunkSprite; // The sprite to represent the chunk visually on the map
    private ChunkType _chunkType; // Enum to define the chunk type (e.g., None, Path, Event)
    private EventType _eventType; // Reference to the ScriptableObject defining the event type (if applicable)
    public int _mapChunkSize; // Size of the chunk, e.g., 8x8 tiles
    // Method to initialize the chunk based on its blueprint and assigned tiles
    public void Init(int mCSize,string mCName, List<Tile> assignedTiles)
    {
        _chunkType = ChunkType.None;
        _tiles = assignedTiles;
        _mapChunkName = mCName;
        _mapChunkSize = mCSize;
        // Reparent the tiles to this chunk
        ReparentTiles();
    }

    // Method to set visual properties of the chunk, such as background sprite
    private void SetChunkVisuals()
    {
        //Check each tile in the chunk to determine dominant environmentType
        //Query one of the dominant tiles to get the large sprite
        //Set the _mapChunkSprite in this instance.

    }

    // Method to set the type of the chunk (e.g., none, path, event)
    public void SetChunkType(ChunkType chunkType)
    {
        _chunkType = chunkType;
    }

    // Method to set the event type for this chunk, if it's an event node
    public void SetEventType(EventType eventType)
    {
        if (_chunkType == ChunkType.Event)
        {
            _eventType = eventType;
        }
        else
        {
            _eventType = null;
        }
    }

    // Method to reparent all tiles to this chunk
    private void ReparentTiles()
    {
        foreach (Tile tile in _tiles)
        {
            tile.transform.SetParent(transform); // Set the parent of each tile to the MapChunk's transform
        }
    }
}

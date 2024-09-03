1. Gridmanager accepts various parameters for Grid (height, width, possibleEnvironmentTypes, chunkSize, etc)
2. Gridmanager creates tiles onmap using perlin noise to determine tile type.
3. When GridManager creates tiles, it adds them to a list _tiles.
4. After all tiles are created, it passes the list to a ChunkManager class that gets added to the MapObject parent in Unity.
5. ChunkManager takes the list, the chunkSize, the width and height, and creates chunks
6. ChunkManager then loops through chunks and is supposed to add tiles relevant to the chunk and its size.

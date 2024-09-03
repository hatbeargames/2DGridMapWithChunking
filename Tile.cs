using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer; // Renderer to handle the tile's visual representation

    public EnvironmentType _environmentType; // Reference to the ScriptableObject defining the environment type
    public Sprite background; // Background sprite for the tile, set based on environment type
    private float _perlinValue; // Perlin noise value for procedural generation or terrain differentiation
    
    //Scenery Child Object for details
    [SerializeField] private GameObject _scenery;
    [SerializeField] private SpriteRenderer _scenerySR;
    // Method to initialize the tile based on its blueprint
    public void Init(float tilePerlin, EnvironmentType envType)
    {


        // Initialize properties based on the blueprint
        _environmentType = envType;
        _perlinValue = tilePerlin;
        _renderer.color = _environmentType.baseColor;
        // Set the renderer's color or sprite based on the environment type from the blueprint
        if (_renderer != null)
        {
            _renderer.color = _environmentType.baseColor; // Use base color from environment type
            _scenerySR.sprite = _environmentType.GetRandomScenerySprite(false); // Get a piece of scenery to add by enviornment type.
            _scenery.SetActive(true); //Enable the scenery object.
        }

    }

    // Method to initialize additional properties or components, if any

    // Example: Update method for dynamic tiles, if needed
    private void Update()
    {
        // Handle any updates per frame if required (e.g., animated tiles, environmental effects)
    }
}

using UnityEngine;

public class WorldController : MonoBehaviour {

    public static WorldController Instance { get; protected set; }  // singleton

    public World world { get; protected set; }  // The world and tile data


    // Use this for initialization
    void OnEnable() {
        if (Instance != null) {
            Debug.LogError("There should never be two world controllers.");
        }
        Instance = this;

        // Create a world with Empty tiles
        world = new World();

        // Center the Camera
        Camera.main.transform.position = new Vector3(world.Width / 2, world.Height / 2, Camera.main.transform.position.z);
    }

    public Tile GetTileAtWorldCoord(Vector3 coord) {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);

        return world.GetTileAt(x, y);
    }
}

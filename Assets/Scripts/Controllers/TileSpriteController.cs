using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileSpriteController : MonoBehaviour {

    public Sprite floorSprite;
    public Sprite emptySprite;
    public Sprite wallSprite;

    Dictionary<Tile, GameObject> tileGameObjectMap;

    World world {
        get { return WorldController.Instance.world; }
    }

    // Use this for initialization
    void Start() {
        tileGameObjectMap = new Dictionary<Tile, GameObject>();

        for (int x = 0; x < world.Width; x++) {
            for (int y = 0; y < world.Height; y++) {
                Tile tile_data = world.GetTileAt(x, y);

                GameObject tile_go = new GameObject();

                tileGameObjectMap.Add(tile_data, tile_go);

                tile_go.name = "Tile" + x + "_" + y;
                tile_go.transform.position = new Vector3(tile_data.X, tile_data.Y, 0);
                tile_go.transform.SetParent(this.transform, true);

                SpriteRenderer sr = tile_go.AddComponent<SpriteRenderer>();
                sr.sprite = emptySprite;
                //sr.sortingLayerName = "Tiles";
            }
        }
        world.RegisterTileChanged(OnTileChanged);
        LoadLevel();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTileChanged(Tile tile_data) {


        if (tileGameObjectMap.ContainsKey(tile_data) == false) {
            Debug.LogError("tileGameObjectMap doesn't contain the tile_data -- did you forget to add the tile to the dictionary? Or maybe forget to unregister a callback?");
            return;
        }

        GameObject tile_go = tileGameObjectMap[tile_data];

        if (tile_go == null) {
            Debug.LogError("tileGameObjectMap's returned GameObject is null -- did you forget to add the tile to the dictionary? Or maybe forget to unregister a callback?");
            return;
        }

        if (tile_data.Type == TileType.Floor) {
            tile_go.GetComponent<SpriteRenderer>().sprite = floorSprite;
        }
        else if (tile_data.Type == TileType.Empty) {
            tile_go.GetComponent<SpriteRenderer>().sprite = emptySprite;
        }
        else if (tile_data.Type == TileType.Wall) {
            tile_go.GetComponent<SpriteRenderer>().sprite = wallSprite;
        }
        else {
            Debug.LogError("OnTileTypeChanged - Unrecognized tile type.");
        }


    }

    void LoadLevel() {

        Debug.Log("SetupPathfindingExample");

        // Make a set of floors/walls to test pathfinding with.

        int l = world.Width / 2 - 5;
        int b = world.Height / 2 - 5;

        world.tiles[0, 0].Type = TileType.Wall;

        for (int x = l - 5; x < l + 15; x++) {
            for (int y = b - 5; y < b + 15; y++) {
                world.tiles[x, y].Type = TileType.Floor;


                if (x == l || x == (l + 9) || y == b || y == (b + 9)) {
                    if (x != (l + 9) && y != (b + 4)) {
                        world.tiles[x, y].Type = TileType.Wall;
                    }
                }
            }
        }
    }

}

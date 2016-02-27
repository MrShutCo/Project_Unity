using UnityEngine;
using System;

public class World {

    public Tile[,] tiles;

    public int Width { get; protected set; }    // The tile width of the world.
    public int Height { get; protected set; }   // The tile height of the world

    Action<Tile> cbTileChanged;


    public World(int width = 100, int height = 100) {
        Width = width;
        Height = height;

        tiles = new Tile[Width, Height];

        for (int x = 0; x < Width; x++) {
            for (int y = 0; y < Height; y++) {
                tiles[x, y] = new Tile(this, x, y);
                tiles[x, y].RegisterTileTypeChangedCallback(OnTileChanged);
            }
        }

        Debug.Log("World created with " + (Width * Height) + " tiles.");
    }

    public void RegisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged += callbackfunc;
    }

    public void UnregisterTileChanged(Action<Tile> callbackfunc) {
        cbTileChanged -= callbackfunc;
    }

    // Gets called whenever ANY tile changes
    void OnTileChanged(Tile t) {
        if (cbTileChanged == null)
            return;

        cbTileChanged(t);

        //InvalidateTileGraph();
    }

    public Tile GetTileAt(int x, int y) {
        if (x >= Width || x < 0 || y >= Height || y < 0) {
            //Debug.LogError("Tile ("+x+","+y+") is out of range.");
            return null;
        }
        return tiles[x, y];
    }
}

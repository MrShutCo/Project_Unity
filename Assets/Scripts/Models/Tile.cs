using UnityEngine;
using System.Collections;
using System;

public enum TileType { Empty, Floor, Wall };

public class Tile {

    //Return the right tile type, and replaces the old one;
    private TileType _type = TileType.Empty;
    public TileType Type {
        get { return _type; }
        set {
            TileType oldType = _type;
            _type = value;
            // Call the callback and let things know we've changed.

            if (cbTileChanged != null && oldType != _type) {
                cbTileChanged(this);
            }
        }
    }

    public World world { get; protected set; }

    public int X { get; protected set; }
    public int Y { get; protected set; }

    // The function we callback any time our tile's data changes
    Action<Tile> cbTileChanged;

    /// <summary>
	/// Initializes a new instance of the <see cref="Tile"/> class.
	/// </summary>
	/// <param name="world">A World instance.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
    public Tile(World world, int x, int y) {
        this.world = world;
        this.X = x;
        this.Y = y;
    }

    public void RegisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileChanged += callback;
    }

    /// <summary>
    /// Unregister a callback.
    /// </summary>
    public void UnregisterTileTypeChangedCallback(Action<Tile> callback) {
        cbTileChanged -= callback;
    }

}

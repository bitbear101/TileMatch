using Godot;
using System;

namespace EventCallback
{
    public class SetTileTypeEvent : Event<SetTileTypeEvent>
    {
        //The position of the tile used as an identifier
        public Vector2 pos;
        //The type og the tile to return;
        public TileType type;
    }
}


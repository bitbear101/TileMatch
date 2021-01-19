using Godot;
using System;

namespace EventCallback
{
    public class GetTileTypeEvent : Event<GetTileTypeEvent>
    {
        //The position of the tile to get the type of
        public Vector2 pos;
        //The type og the tile to return;
        public TileType type;
    }
}


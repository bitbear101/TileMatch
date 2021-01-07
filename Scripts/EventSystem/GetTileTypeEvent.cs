using Godot;
using System;

namespace EventCallback
{
    public class GetTileTypeEvent : Event<GetTileTypeEvent>
    {
        //The ID of the tile we want the type of
        public ulong id;
        //The type og the tile to return;
        public TileType type;
    }
}


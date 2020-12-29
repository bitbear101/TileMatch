using Godot;
using System;
namespace EventCallback
{
    public class TileDestroyedEvent : Event<TileDestroyedEvent>
    {
        //The instance ID for the tile
        public ulong tileID;
    }

}

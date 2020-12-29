using Godot;
using System;

namespace EventCallback
{
    public class TileTypeChangeEvent : Event<TileTypeChangeEvent>
    {
        public TileType type;
    }
}


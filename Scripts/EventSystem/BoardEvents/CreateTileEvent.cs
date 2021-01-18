using Godot;
using System;
using System.Collections.Generic;
namespace EventCallback
{
    public class CreateTileEvent : Event<CreateTileEvent>
    {
        //The type of tile to create
        public TileType type;
        //The position of creation for he tile class in the board
        public Vector2 pos;
    }
}

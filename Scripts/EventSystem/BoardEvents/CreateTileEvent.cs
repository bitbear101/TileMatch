using Godot;
using System;
using System.Collections.Generic;
namespace EventCallback
{
    public class CreateTileEvent : Event<CreateTileEvent>
    {
        //The refference to the board
        public Node2D[,] board;
        //The list of empty top row tiles
        public List<Vector2> emptyTopRowSlotPos = new List<Vector2>();
    }
}

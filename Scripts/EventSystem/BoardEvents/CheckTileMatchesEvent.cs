using Godot;
using System;
namespace EventCallback
{
    public class CheckTileMatchesEvent : Event<CheckTileMatchesEvent>
    {
        //The refference to the board
        public Node2D[,] board;
        //The position of the tile to check
        public Node2D tile;
    }
}


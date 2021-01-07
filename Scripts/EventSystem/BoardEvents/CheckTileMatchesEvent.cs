using Godot;
using System;
namespace EventCallback
{
    public class CheckTileMatchesEvent : Event<CheckTileMatchesEvent>
    {
        //Check entire board or just 2 seperate tiles that are being swung
        bool checkBoard, checkTiles;
        //The refference to the board
        public Node2D[,] board;
        //The size of the board in tiles
        public Vector2 boardSize;
        //The position of the tile to check
        public Node2D tile;
    }
}


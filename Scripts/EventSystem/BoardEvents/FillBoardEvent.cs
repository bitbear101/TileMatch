using Godot;
using System;
namespace EventCallback
{
    public class FillBoardEvent : Event<FillBoardEvent>
    {
        //The board of tiles
        public Node2D[,] board;
        //The size of the board
        public Vector2 boardSize;
    }
}
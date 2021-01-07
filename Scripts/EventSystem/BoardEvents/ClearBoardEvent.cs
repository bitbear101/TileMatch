using Godot;
using System;
namespace EventCallback
{
    public class ClearBoardEvent : Event<ClearBoardEvent>
    {
        //The board to be cleared
        public Node2D[,] board;
        //The size of the board
        public Vector2 boardSize;
    }
}

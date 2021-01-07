using Godot;
using System;
namespace EventCallback
{
    public class CheckEmptySlotsEvent : Event<CheckEmptySlotsEvent>
    {
        //The reference to the board
        public Node2D[,] board;
        //The reference tothe size of the board in tiles
        public Vector2 boardSize;
    }

}

using Godot;
using System;
namespace EventCallback
{
    public class GetBoardEvent : Event<GetBoardEvent>
    {
        //Get a reference to the board
        public Node2D[,] board;
    }
}

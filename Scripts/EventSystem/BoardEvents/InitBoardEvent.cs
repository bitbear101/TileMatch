using Godot;
using System;
namespace EventCallback
{
    public class InitBoardEvent : Event<InitBoardEvent>
    {
        //The board of tiles
        public Tile[,] board;
    }
}
using Godot;
using System;
namespace EventCallback
{
    public class InitVisualBoardEvent : Event<InitVisualBoardEvent>
    {
        //The board of tiles
        public Tile[,] board;
    }
}
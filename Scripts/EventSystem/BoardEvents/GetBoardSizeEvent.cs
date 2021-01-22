using Godot;
using System;
namespace EventCallback
{
    public class GetBoardSizeEvent : Event<GetBoardSizeEvent>
    {
        //The boards size in tile units not pixels
        public int boardSizeX, boardSizeY;
    }
}

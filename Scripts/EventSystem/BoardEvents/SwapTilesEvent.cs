using Godot;
using System;

namespace EventCallback
{
    public class SwapTilesEvent : Event<SwapTilesEvent>
    {
        //The direction of the swipe
        //Up (0,-1);
        //Down (0, 1)
        //Left (-1, 0)
        //Right (1, 0)
        public Vector2 swipeDirection;
        //The instance Id for the tile that the swipe was started on
        public ulong tileID;
    }
}
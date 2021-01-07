using Godot;
using System;

namespace EventCallback
{
    public class SwapTilesEvent : Event<SwapTilesEvent>
    {
        //The tiles that are swapped
        public Node2D[] tiles = new Node2D[2];
    }
}
using Godot;
using System;

namespace EventCallback
{
    public class SwapTilesEvent : Event<SwapTilesEvent>
    {
        //The start and end position of the drag
        public Vector2 dragStartPos, dragEndPos;  
    }
}
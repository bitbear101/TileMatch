using Godot;
using System;

namespace EventCallback
{
    public class SwapTilesEvent : Event<SwapTilesEvent>
    {
        //The instance IS for the tile (uneque identefier for the object)
        public ulong tileID;
        //The start and end position of the drag
        public Vector2 dragStartPos, dragEndPos;
        
    }
}
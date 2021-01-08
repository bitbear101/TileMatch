using Godot;
using System;

namespace EventCallback
{
    public class InputManagerEvent : Event<InputManagerEvent>
    {
        //The name of the object that was tapped
        public ulong nodeID;
        public Vector2 dragStartPos, dragEndPos;
    }
}

using Godot;
using System;

namespace EventCallback
{
    public class InputHandleEvent : Event<InputHandleEvent>
    {
        //The name of the object that was tapped
        public ulong nodeID;
        public Vector2 touchPosition;
        public Vector2 dragedDirection;
    }
}

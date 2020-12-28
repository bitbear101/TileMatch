using Godot;
using System;

namespace EventCallback
{
    public class WinEvent : Event<WinEvent>
    {
        public bool won;
    }
}


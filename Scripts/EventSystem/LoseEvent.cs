using Godot;
using System;

namespace EventCallback
{
    public class LoseEvent : Event<LoseEvent>
    {
        public bool lost;
    }
}
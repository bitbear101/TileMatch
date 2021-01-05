using Godot;
using System;
namespace EventCallback
{
    public class TileStateChangeEvent : Event<TileStateChangeEvent>
    {
        //The new state to be broughtcast as an event message
        public TileState newState;
    }
}

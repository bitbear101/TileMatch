using Godot;
using System;
namespace EventCallback
{
    public class BoardStateChangeEvent : Event<BoardStateChangeEvent>
    {
        //The new state to be broughtcast as an event message
        public BoardState newState;
    }
}

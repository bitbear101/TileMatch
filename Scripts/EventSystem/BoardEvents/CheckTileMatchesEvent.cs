using Godot;
using System;
namespace EventCallback
{
    public class CheckTileMatchesEvent : Event<CheckTileMatchesEvent>
    {
        //The position of the tile to check
        public Vector2 tilePos;
        //The bool to state if there was any tile matches
        public bool matches = false;
    }
}


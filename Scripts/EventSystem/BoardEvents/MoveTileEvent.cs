using Godot;
using System;
using System.Collections.Generic;
namespace EventCallback
{
    public class MoveTileEvent : Event<MoveTileEvent>
    {
        //Activate ar deactivate the tile move class
        public bool active;
        //Stores the empty tile positions
        public List<Vector2> emptySlotPos = new List<Vector2>();

    }

}

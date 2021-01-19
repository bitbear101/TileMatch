using Godot;
using System;
namespace EventCallback
{
    public class CreateTileEvent : Event<CreateTileEvent>
    {
        //The position of the tile to be created
        public Vector2 pos;
    }

}

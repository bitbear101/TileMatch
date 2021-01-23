using Godot;
using System;
namespace EventCallback
{
    public class GetTileSizeEvent : Event<GetTileSizeEvent>
    {
        //The size of the tile in pixels
        public int size;
    }
}
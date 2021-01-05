using Godot;
using System;
namespace EventCallback
{
    public class CreateTileEvent : Event<CreateTileEvent>
    {
        //The position of the tile on the board where it is needed when created
        Vector2 boardPos;
    }
}

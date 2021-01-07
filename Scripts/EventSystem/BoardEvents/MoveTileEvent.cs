using Godot;
using System;
using System.Collections.Generic;
namespace EventCallback
{
    public class MoveTileEvent : Event<MoveTileEvent>
    {
        //Activate ar deactivate the tile move class
        public bool active;
        //The reference to the board
        public Node2D[,] board;
        //The reference tothe size of the board in tiles
        public Vector2 boardSize;
        
        //Stores the empty tile positions
        public List<Vector2> emptySlotPos = new List<Vector2>();
        public List<Vector2> emptyTopRowSlotPos = new List<Vector2>();
    }

}

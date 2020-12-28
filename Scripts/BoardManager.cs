using Godot;
using System;

public class BoardManager : Node2D
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    int[,] boardSize = new int[boardWidth - 1, boardHeight - 1];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Loop through the board
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                GD.Print("(x: " + boardWidth + " y: " + boardHeight + ") TileType - ");
            }
        }
    }

        public void checkTileNeighbours(Vector2 pos)
    {
        //Check all the surrounding tiles of the target tile
        for (int x = (int)pos.x - 1; x < (int)pos.x + 1; x++)
        {
            for (int y = (int)pos.y - 1; y < (int)pos.y + 1; y++)
            {
                
            }
        }
    }

    public void genTile(Vector2 pos)
    {

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}


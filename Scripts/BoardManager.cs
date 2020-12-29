using Godot;
using System;
using EventCallback;
public class BoardManager : Node2D
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    Node2D[,] boardSize = new Node2D[boardWidth - 1, boardHeight - 1];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        //Run when the board is created the first time and whenever we want to reset the board
        ResetMap();
    }
    //Not a usefull method for now
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
    //Called the first time to populate the map
    private void ResetMap()
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

    private void UpdateMap()
    {

    }

    private void OnTileDestroyedEvent(TileDestroyedEvent tdei)
    {
        //When the tile is destroyed update the map
        GD.InstanceFromId(tdei.tileID);
    }

}


using Godot;
using System;
using EventCallback;
public class Board : Node
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    Tile[,] board = new Tile[boardWidth, boardHeight];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Called when a tile is destroyed
        //TileDestroyedEvent.RegisterListener(OnTileDestroyedEvent);
        //Register the swap tile event listener
        GetBoardEvent.RegisterListener(OnGetBoardEvent);
    }
    //When there is a request for the board
    private void OnGetBoardEvent(GetBoardEvent gbei)
    {
        //Return a refference of the board to the requester
        gbei.board = board;
    }
}

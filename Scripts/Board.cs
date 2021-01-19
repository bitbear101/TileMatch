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
        //Register the listener for hte get tile type event
        GetTileTypeEvent.RegisterListener(OnGetTileTypeEvent);

        //Register the swap tile event listener
        GetBoardEvent.RegisterListener(OnGetBoardEvent);
    }
    //When there is a request for the board
    private void OnGetBoardEvent(GetBoardEvent gbei)
    {
        //Return a refference of the board to the requester
        gbei.board = board;
    }
    private void OnGetTileTypeEvent(GetTileTypeEvent gttei)
    {
        //Get the tile type at the position of the tile
        gttei.type = board[(int)gttei.pos.x, (int)gttei.pos.y].Type;
    }
}

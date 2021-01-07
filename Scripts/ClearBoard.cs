using Godot;
using System;
using EventCallback;
public class ClearBoard : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Regester this listener to the clear board event
        ClearBoardEvent.RegisterListener(OnClearBoardEvent);
    }
    private void OnClearBoardEvent(ClearBoardEvent cbei)
    {
        //Loop through the entire board
        for (int x = 0; x < cbei.boardSize.x; x++)
        {
            for (int y = 0; y < cbei.boardSize.x; y++)
            {
                //Delete the tile object
                cbei.board[x, y].QueueFree();
                //Set the position in the list for the tile to null
                cbei.board[x, y] = null;
            }
        }
    }
}

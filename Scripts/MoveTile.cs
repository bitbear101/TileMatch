using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class MoveTile : Node2D
{
    //Stores the empty tile positions
    List<Vector2> emptySlotPos = new List<Vector2>();
    //The empty top row of slots positions list
    List<Vector2> emptyTopRowSlotPos = new List<Vector2>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the listener for the move tile event
        MoveTileEvent.RegisterListener(OnMoveTileEvent);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public void OnMoveTileEvent(MoveTileEvent mtei)
    {
        GD.Print("BoardManager - MoveTile: Running");
        //Get the tile class board array
        GetBoardEvent gbei = new GetBoardEvent();
        gbei.FireEvent();

        //The empty slots array to interate through
        emptySlotPos = mtei.emptySlotPos;

        if (emptySlotPos.Count > 0)
        {
            //Loop through all the empty slot positions
            for (int i = 0; i < emptySlotPos.Count; i++)
            {
                //Loop through the whole column of slots from the bottom at the empty slot to the top of the board
                for (int y = (int)emptySlotPos[i].y; y > 0; y--)
                {
                    //Set the empty slot to the slot aboves tile
                    gbei.board[(int)emptySlotPos[i].x, y].Type = gbei.board[(int)emptySlotPos[i].x, y - 1].Type;
                    //Set the tiles it just copieds slot to null
                    gbei.board[(int)emptySlotPos[i].x, y - 1].Type = TileType.NONE;
                }
            }
        }

        //Once the tile is in position the state is changed back to the check for voids state
        BoardStateChangeEvent bscei = new BoardStateChangeEvent();
        bscei.newState = BoardState.CHECK_VOIDS;
        bscei.FireEvent();
        GD.Print("BoardManager - MoveTile: Done");

    }
}

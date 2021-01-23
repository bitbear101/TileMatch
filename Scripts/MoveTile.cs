using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class MoveTile : Node
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
        //Call the event message to get the boards size
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        gbsei.FireEvent();

        //Send a message to the event callback to get the tile type
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //Send a message to the event callback for the set tile type event
        SetTileTypeEvent sttei = new SetTileTypeEvent();

        //The empty slots array to interate through
        emptySlotPos = mtei.emptySlotPos;

        if (emptySlotPos.Count > 0)
        {
            //Loop through all the empty slot positions
            for (int i = 0; i < emptySlotPos.Count; i++)
            {
                //Loop through the whole column of slots from the bottom starting at the empty slot to the top of the board
                for (int y = (int)emptySlotPos[i].y; y > 0; y--)
                {
                    //Get the tile from the get tile event callback to get the tile type above the empty slot
                    gttei.pos = new Vector2(emptySlotPos[i].x, (y - 1));
                    gttei.FireEvent();

                    //Call the set tile event callback message to set the empty slot to the tile type above its value
                    sttei.pos = new Vector2(emptySlotPos[i].x, y);
                    sttei.type = gttei.type;
                    sttei.FireEvent();

                    //Call the set tile event callback message to clear the tile aboves value
                    sttei.pos = new Vector2(emptySlotPos[i].x, y - 1);
                    sttei.type = TileType.NONE;
                    sttei.FireEvent();
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

using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class CheckEmptySlots : Node
{
    //This method gets run the first time the class is instantiated
    public override void _Ready()
    {
        //Regestir the listener for hte Check empty slots event
        CheckEmptySlotsEvent.RegisterListener(OnCheckEmptySlotsEvent);
    }


    public void OnCheckEmptySlotsEvent(CheckEmptySlotsEvent cesei)
    {
        GD.Print("BoardManager - CheckForEmptySlots: Running");
        //Get the boards size in tiles not pixels
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        gbsei.FireEvent();
        //Get the tile type
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //Stores the empty tile positions
        List<Vector2> emptySlotPos = new List<Vector2>();
        List<Vector2> emptyTopRowSlotPos = new List<Vector2>();
        //If any void tiles are found in the main body of the board
        bool emptySlots = false;
        //If any void tiles are found in the top row of the board
        bool emptyTopRowSlots = false;
        //Clear the empty tile lists
        emptySlotPos.Clear();
        emptyTopRowSlotPos.Clear();
        //Find open position and call tile move function on the tile above it

        //Loop through the board from bottom to top to add the bottom most empty slots into the array first so they drop first 
        for (int y = gbsei.boardSizeY - 1; y > -1; y--)
        {
            //Loop through the board
            for (int x = 0; x < gbsei.boardSizeX; x++)
            {
                gttei.pos = new Vector2(x, y);
                gttei.FireEvent();
                //If the slot on the board is empty
                if (gttei.type == TileType.NONE)
                {
                    //If the empty slot is not in thte top row
                    if (y != 0)
                    {
                        //If the emptySlotPos has an entry
                        if (emptySlotPos.Count > 0)
                        {
                            //Loop through all the empty slot position
                            for (int i = 0; i < emptySlotPos.Count; i++)
                            {
                                //If the empty slot is not in the same column as any of the others in the list 
                                if (emptySlotPos[i].x != x)
                                {
                                    //There is an empty slot in the board we set it to true
                                    emptySlots = true;
                                    //We add hte empty slots position to the list
                                    emptySlotPos.Add(new Vector2(x, y));
                                }
                            }
                        }
                        else
                        {
                            //There is an empty slot in the board we set it to true
                            emptySlots = true;
                            //We add hte empty slots position to the list
                            emptySlotPos.Add(new Vector2(x, y));
                        }
                    }
                    else
                    {
                        //There is an empty spot in the board we set it to true
                        emptyTopRowSlots = true;
                        //Add the top row empty slot to the empty top row list
                        emptyTopRowSlotPos.Add(new Vector2(x, y));
                    }
                }
            }
        }
        //If there where tile positions with no tiles in we go to the move tiles state else we go to the wait state
        if (emptySlots)
        {
            GD.Print("BoardManager - CheckForEmptySlots: Calling move tiles method");
            //If there are empty slots send a message to the move tile event
            MoveTileEvent mtei = new MoveTileEvent();
            //Set the move tile class to active
            mtei.active = true;
            //We pass in the list of empty slots to the move tile class
            mtei.emptySlotPos = emptySlotPos;
            //We send the events message to all the listeners
            mtei.FireEvent();
        }
        //If the empty slots where top row tiles we need to create new tiles
        else if (emptyTopRowSlots)
        {
            //Send the create tile event message
            CreateTileEvent ctei = new CreateTileEvent();

            foreach (Vector2 tilePos in emptyTopRowSlotPos)
            {
                //Send the tiles position to be created
                ctei.pos = new Vector2(tilePos);
                ctei.FireEvent();
            }
        }
        else
        {
            //Set the boards state to move tile after collecting all the open spaces
            BoardStateChangeEvent bscei = new BoardStateChangeEvent();
            //Set the new state message for 
            bscei.newState = BoardState.WAIT;
            //Send the event message
            bscei.FireEvent();
            GD.Print("BoardManager - CheckForEmptySlots: Change board State to WAIT");
        }
        GD.Print("BoardManager - CheckForEmptySlots: Done");
    }
}

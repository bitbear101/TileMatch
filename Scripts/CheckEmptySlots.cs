using Godot;
using System;
using EventCallback;
public class CheckEmptySlots : Node
{
    public void OnCheckEmptySlotsEvent()
    {
        GD.Print("BoardManager - CheckForEmptySlots: Running");
        //If any void tiles are found set it to true
        bool emptySlots = false;
        //Clear the empty tile lists
        emptySlotPos.Clear();
        emptyTopRowSlotPos.Clear();
        //Find open position and call tile move function on the tile above it

        //Loop through the board from bottom to top to add the bottom most empty slots into the array first so they drop first 
        for (int y = boardHeight - 1; y > -1; y--)
        {
            //Loop through the board
            for (int x = 0; x < boardWidth; x++)
            {
                //If the slot on the board is empty
                if (boardTiles[x, y] == null)
                {
                    GD.Print("BoardManager - CheckForEmptySlots: x, y = " + x + ", " + y);
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
                        emptySlots = true;
                        //Add the top row empty slot to the empty top row list
                        emptyTopRowSlotPos.Add(new Vector2(x, y));
                    }
                }
            }
        }
        //If there where tile positions with no tiles in we go to the move tiles state else we go to the wait state
        if (emptySlots)
        {
            //Set the boards state to move tile after collecting all the open spaces
            ChangeState(BoardState.MOVETILES);
            GD.Print("BoardManager - CheckForEmptySlots: Change board State to MOVETILES");

        }
        else
        {
            //Set the boards state to move tile after collecting all the open spaces
            ChangeState(BoardState.WAIT);
            GD.Print("BoardManager - CheckForEmptySlots: Change board State to WAIT");
        }
        GD.Print("BoardManager - CheckForEmptySlots: Done");
    }


}

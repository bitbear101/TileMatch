using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class MoveTile : Node
{
    //If the state is active
    bool active = false;
    //The reference tothe board
    Node2D[,] board;
    //The reference to the size of the board
    Vector2 boardSize;
    //Stores the empty tile positions
    List<Vector2> emptySlotPos = new List<Vector2>();
    List<Vector2> emptyTopRowSlotPos = new List<Vector2>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the listener for the move tile event
        MoveTileEvent.RegisterListener(OnMoveTileEvent);

    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //If the state is not active we return out of the loop
        if (!active) return;
        //If the state is active we call the MoveTiles method
        MoveTiles();
    }
    public void OnMoveTileEvent(MoveTileEvent mtei)
    {        
        //Set the move tile as active
        active = mtei.active;
        //Set the perameters needed for this method to work
        board = mtei.board;
        boardSize = mtei.boardSize;
        emptySlotPos = mtei.emptySlotPos;
    }
    public void MoveTiles()
    {
        GD.Print("BoardManager - MoveTile: Running");
        if (emptySlotPos.Count > 0)
        {
            //Loop through all the empty slot positions
            for (int i = 0; i < emptySlotPos.Count; i++)
            {
                //Loop through the whole column of slots from the top of the empty slot to the top of the board
                for (int y = (int)emptySlotPos[i].y; y > 0; y--)
                {
                    //Set the empty slot to the slot aboves tile
                    board[(int)emptySlotPos[i].x, y] = board[(int)emptySlotPos[i].x, y - 1];
                    //Set the tiles position in the world
                    board[(int)emptySlotPos[i].x, y - 1].Position = new Vector2(emptySlotPos[i].x * 32, y * 32);
                    //Set the tiles it just copieds slot to null
                    board[(int)emptySlotPos[i].x, y - 1] = null;
                }
            }
            active = false;
        }        
        /*
        1. Get the whole column of tiles above the empty tile
        2. Intereate through the column and move them doward
        */
        /*
        //Set the tiles new position to be used in the linear interpolate 
        //Vector2 newTilePosition = new Vector2(tileToDropPos[i].x, tileToDropPos[i].y + 32);

        -- THE NICE LOOKING TILE MOVEMENT CAN COME LATER FIRST GET THE TILES GOING TO THE RIGHT PLACES
        Linearly interpolate between the tiles current position and the tile new position
        //tilesToDrop[i].Position = tilesToDrop[i].Position.LinearInterpolate(newTilePosition, .5f);

        //If the tile is close enough to the target position
        if (tilesToDrop[i].Position.DistanceTo(newTilePosition) < .05f)
        {
            //We set the tile to its final position
            tilesToDrop[i].Position = newTilePosition;
            //Set the tiles new position in the board array
            boardTiles[(int)newTilePosition.x / 32, (int)newTilePosition.y / 32] = tilesToDrop[i];
            boardTiles[(int)newTilePosition.x / 32, (int)(newTilePosition.y - 32) / 32] = null;
        }
        */


        //Once the tile is in position the state is changed back to the check for voids state
        BoardStateChangeEvent bscei = new BoardStateChangeEvent();
        bscei.newState = BoardState.CHECK_VOIDS;
        bscei.FireEvent();
        GD.Print("BoardManager - MoveTile: Done");

    }
}

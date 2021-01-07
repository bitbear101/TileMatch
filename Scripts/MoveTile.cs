using Godot;
using System;

public class MoveTile : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

 // Called every frame. 'delta' is the elapsed time since the previous frame.
 public override void _Process(float delta)
 {
     
 }

public void DropTile()
    {
        GD.Print("BoardManager - DropTile: Running");
        if (emptySlotPos.Count > 0)
        {
            //Loop through all the empty slot positions
            for (int i = 0; i < emptySlotPos.Count; i++)
            {
                //Loop through the whole column of slots from the top of the empty slot to the top of the board
                for (int y = (int)emptySlotPos[i].y; y > 0; y--)
                {
                    //Set the empty slot to the slot aboves tile
                    boardTiles[(int)emptySlotPos[i].x, y] = boardTiles[(int)emptySlotPos[i].x, y - 1];
                    //Set the tiles position in the world
                    boardTiles[(int)emptySlotPos[i].x, y - 1].Position = new Vector2(emptySlotPos[i].x * 32, y * 32);
                    //Set the tiles it just copieds slot to null
                    boardTiles[(int)emptySlotPos[i].x, y - 1] = null;
                }
            }
        }

        if (emptyTopRowSlotPos.Count > 0)
        {
            //Interate through the top row of the board and spawn new tiles in
            for (int j = 0; j < emptyTopRowSlotPos.Count; j++)
            {
                //Instantiate the tilscene and set the boards node to the scene instanced
                boardTiles[(int)emptyTopRowSlotPos[j].x, (int)emptyTopRowSlotPos[j].y] = ((Node2D)tileScene.Instance());
                //Set the position of the tile on the board representation in the viewport
                boardTiles[(int)emptyTopRowSlotPos[j].x, (int)emptyTopRowSlotPos[j].y].Position = new Vector2((int)emptyTopRowSlotPos[j].x * 32, (int)emptyTopRowSlotPos[j].y * 32);
                //Add the tile as a child to the board scene
                AddChild(boardTiles[(int)emptyTopRowSlotPos[j].x, (int)emptyTopRowSlotPos[j].y]);
            }
        }

        /*
        1. Get the whole column of tiles above the empty tile
        2. Intereate through the column and move them doward
        */
        //Set the tiles new position to be used in the linear interpolate 
        //Vector2 newTilePosition = new Vector2(tileToDropPos[i].x, tileToDropPos[i].y + 32);

        /* -- THE NICE LOOKING TILE MOVEMENT CAN COME LATER FIRST GET THE TILES GOING TO THE RIGHT PLACES
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
        ChangeState(BoardState.CHECKVOIDS);
        GD.Print("BoardManager - DropTile: Done");
    }
}

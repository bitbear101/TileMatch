using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

/*
Order of process:
1. Detect any open positions on the board
2. Move all the tile on top of the gap down until no more open positions exist
3. Spawn a new tile if the position on the top line is open
4. Let the new spawned tiles move to fill positions
5. When all positions on the map are filled check for 3 or more tiles in sequence
6. Remove matching tiles
7. If tiles where removed repeat from step one. 
*/

public enum BoardState
{
    FILLBOARD,
    CLEARBOARD,
    CHECKVOIDS,
    MOVETILES,
    WAIT
};

public class BoardManager : Node2D
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    Node2D[,] boardTiles = new Node2D[boardWidth, boardHeight];
    //The tile that needs to be dropped
    List<Node2D> columnToDrop = new List<Node2D>();
    //The list of empty positions on the board
    List<Vector2> emptySlotPos = new List<Vector2>();
    //If any of the top row slots are empty we add them to this list as new tiles need to spawned
    List<Vector2> emptyTopRowSlotPos = new List<Vector2>();
    //The scene for the tile object
    PackedScene tileScene;
    //The state fore the board
    BoardState state;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
        //Set the state of the board to fill it
        ChangeState(BoardState.FILLBOARD);
    }
    public override void _Process(float delta)
    {
        switch (state)
        {
            case BoardState.FILLBOARD:
            GD.Print("BoardManager - _Process: Running State FILLBOARD");
                //Run when the board is created the first time and whenever we want to reset the board
                FillBoard();
                //Afte the fill board state has benn run we switch to the wait state until usr iput changes the board status
                ChangeState(BoardState.CHECKVOIDS);
                break;

            case BoardState.WAIT:
            GD.Print("BoardManager - _Process: Running State WAIT");
                break;

            case BoardState.CHECKVOIDS:
            GD.Print("BoardManager - _Process: Running State CHECKVOIDS");
                //Check for voids (empty spaces) in the board
                CheckForEmptySlots();
                break;

            case BoardState.MOVETILES:
            GD.Print("BoardManager - _Process: Running State MOVETILES");
                //Move the tiles down
                DropTile();
                break;

            case BoardState.CLEARBOARD:
            GD.Print("BoardManager - _Process: Running State CLEARBOARD");
                //Clears the board of tiles
                ClearBoard();
                //After clearing the board the wait state is run waiting for user input or something I hope
                ChangeState(BoardState.WAIT);
                break;
        }
    }

    //Return the tile at the requested position
    private Node2D GetTile(Vector2 pos)
    {
        //Return the tile node at the requested position
        return boardTiles[(int)pos.x, (int)pos.y];
    }
    public void RemoveTileAt(Vector2 pos)
    {
        //Delete the tile object
        boardTiles[(int)pos.x, (int)pos.y].QueueFree();
        //Set the position in the list for the tile to null
        boardTiles[(int)pos.x, (int)pos.y] = null;
    }

    // //Check for matching neighbouring tiles
    // public List<Node2D> CheckMatchingNeighbours(Node2D tile)
    // {
    //     //Checking tile matches seperately on the x and then the y axis makes sure we only check
    //     //for vertical and horizontal matches only
    //     //Check all the x position tiles first
    //     for (int x = (int)tile.Position.x - 1; x < (int)tile.Position.x + 1; x++)
    //     {
    //         //Make sure the tile is not checking against itself by making sure the position is not the same
    //         if(tile.Position != x)
    //         {
    //             //Get the script on the node object and then grab the tile types from that for a comparison
    //             if(((Tile)tile).type == ((Tile)GetTile(new Vector2(x,y))).type)
    //             {

    //             }
    //         }
    //     }            
    //     //Check all the y position tiles 
    //     for (int y = (int)tile.Position.y - 1; y < (int)tile.Position.y + 1; y++)
    //     {
    //         if(tile.Position.y != y)
    //         {
    //             //Get the script on the node object and then grab the tile types from that for a comparison
    //             if(((Tile)tile).type == ((Tile)GetTile(new Vector2(x,y))).type)
    //             {

    //             }
    //         }

    //     }
    // }

    public void CheckForEmptySlots()
    {
        GD.Print("BoardManager - CheckForEmptySlots: Running");
        //If any void tiles are found set it to true
        bool emptySlots = false;
        //Find open position and call tile move function on the tile above it
        //Loop through the board
        for (int x = 0; x < boardWidth; x++)
        {
            //Loop through the board from bottom to top to add the bottom most empty slots into the array first so they drop first 
            for (int y = boardHeight - 1; y > 0; y--)
            {
                //If the slot on the board is empty
                if (boardTiles[x, y] == null)
                    //If the empty slot is not in thte top row
                    if (y != 0)
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
                        //There is an empty spot in the board we set it to true
                        emptySlots = true;
                        //Add the top row empty slot to the empty top row list
                        emptyTopRowSlotPos.Add(new Vector2(x, y));
                    }
            }
        }
        //If there where tile positions with no tiles in we go to the move tiles state else we go to the wait state
        if (emptySlots)
        {
            //Set the boards state to move tile after collecting all the open spaces
            ChangeState(BoardState.MOVETILES);
        }
        else
        {
            //Set the boards state to move tile after collecting all the open spaces
            ChangeState(BoardState.WAIT);
            GD.Print("BoardManager - CheckForEmptySlots: No void tiles found state changed to WAIT state");
        }
GD.Print("BoardManager - CheckForEmptySlots: Done");
    }

    public void DropTile()
    {
        GD.Print("BoardManager - DropTile: Running");
        //Loop through all the empty slot positions
        for (int i = 0; i < emptySlotPos.Count; i++)
        {
            //Loop through the whole column of slots from bottom to top
            for (int y = (int)emptySlotPos[i].y; y > 0; y++)
            {
                //If hte slot we are trying to fill is not the top most slot
                if (y != 0)
                {
                    //Set the empty slot to the tile above in the boards array
                    boardTiles[(int)emptySlotPos[y].x, y] = boardTiles[(int)emptySlotPos[y].x, y - 1];
                    //Set the tiles position in the world
                    boardTiles[(int)emptySlotPos[y].x, y - 1].Position = new Vector2(emptySlotPos[y].x, y * 32);
                }
                else
                {
                    //If the slot is the top most slot we set it to null
                    boardTiles[(int)emptySlotPos[y].x, y] = null;
                }
            }
        }

        //Interate through the top row of the board and spawn new tiles in
        for (int j = 0; j < emptyTopRowSlotPos.Count; j++)
        {
            GD.Print("BoardManager - DropTile: Top row slot open");
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

    private void CheckTileMatches()
    {
        //The open list for the tiles that need to be checked
        List<Node2D> openTileList = new List<Node2D>();
        //The closed list for tiles that have already been checked
        List<Node2D> closeTileList = new List<Node2D>();

        //Loop through the board and add all the tile to the open list
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                openTileList.Add(boardTiles[x, y]);
            }
        }
        //Keep two tile lists, Open and Closed list to keep track of wich tiles have ben check and those that still need to be checked
        //Run though the open list tiles and check thier neibours for matching tiles, all those that match move and have been checked move them to the closed list
    }
    //Called the first time to populate the map
    private void ResetBoard()
    {
        //Clear the map of all tiles
        ClearBoard();
        //Refill the board with tiles
        FillBoard();
    }
    private void ClearBoard()
    {
        //Loop through the board
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                //Delete the tile object
                boardTiles[x, y].QueueFree();
                //Set the position in the list for the tile to null
                boardTiles[x, y] = null;
            }
        }
    }
    private void FillBoard()
    {
        GD.Print("BoardManager - FillBoard: Running");
        //Loop through the board
        for (int x = 0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                //Instantiate the tilscene and set the boards node 
                boardTiles[x, y] = ((Node2D)tileScene.Instance());
                boardTiles[x, y].Position = new Vector2(x * 32, y * 32);
                AddChild(boardTiles[x, y]);
            }
        }
        GD.Print("BoardManager - FillBoard: Done");
    }
    public void ChangeState(BoardState newState)
    {
        //Change the current state to the new state
        state = newState;
    }
    private void OnTileDestroyedEvent(TileDestroyedEvent tdei)
    {
        //When the tile is destroyed update the map
        GD.InstanceFromId(tdei.tileID);
    }
}
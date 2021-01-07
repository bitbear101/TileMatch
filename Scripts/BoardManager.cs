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


    //The state fore the board
    BoardState state;
    //A custom setter and getter for the state to send a message to all listeners when it is changed
    public BoardState State
    {
        get { return state; }
        set
        {
            //Set the new value of state
            state = value;
            //When the state is changed call the new state function
            NewState();
        }
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set the state of the board to fill it
        ChangeState(BoardState.FILLBOARD);
    }
    private void NewState()
    {
        switch (state)
        {
            case BoardState.FILLBOARD:
                GD.Print("BoardManager - _Process: Running State FILLBOARD");
                //Send a message to the FillBoardEvent
                FillBoardEvent fbei = new FillBoardEvent();
                //Get a refference to the board
                fbei.board = boardTiles;
                //Send the board size as a Vector2
                fbei.boardSize = new Vector2(boardWidth, boardHeight);
                //Send the event to the listeners
                fbei.FireEvent();

                //Remove tile for testing
                RemoveTileAt(new Vector2(4, 4));

                //Remove tile for testing
                //RemoveTileAt(new Vector2(8, 8));

                //Remove tile for testing
                //RemoveTileAt(new Vector2(1, 1));

                //Afte the fill board state has benn run we switch to the wait state until usr iput changes the board status
                ChangeState(BoardState.WAIT);
                break;

            case BoardState.WAIT:
                GD.Print("BoardManager - _Process: Running State WAIT");
                break;

            case BoardState.CHECKVOIDS:
                GD.Print("BoardManager - _Process: Running State CHECKVOIDS");
                //Check for voids (empty spaces) in the board
                //CheckForEmptySlots();
                break;

            case BoardState.MOVETILES:
                GD.Print("BoardManager - _Process: Running State MOVETILES");
                //Move the tiles down
                //DropTile();
                break;

            case BoardState.CLEARBOARD:
                GD.Print("BoardManager - _Process: Running State CLEARBOARD");
                //Clears the board of tiles
                //ClearBoard();
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
    public void ChangeState(BoardState newState)
    {
        //Change the current state to the new state
        State = newState;
    }
    private void OnTileDestroyedEvent(TileDestroyedEvent tdei)
    {
        //When the tile is destroyed update the map
        GD.InstanceFromId(tdei.tileID);
    }
}
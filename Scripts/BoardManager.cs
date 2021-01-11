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
    FILL_BOARD,
    CLEAR_BOARD,
    CHECK_MATCHES,
    CHECK_VOIDS,
    WAIT
};

public class BoardManager : Node2D
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    Node2D[,] board = new Node2D[boardWidth, boardHeight];
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
        TileDestroyedEvent.RegisterListener(OnTileDestroyedEvent);
        BoardStateChangeEvent.RegisterListener(OnBoardStateChangeEvent);
        //Register the swap tile event listener
        GetBoardEvent.RegisterListener(OnGetBoardEvent);
        //Set the state of the board to fill it
        State = BoardState.FILL_BOARD;
    }
    private void NewState()
    {
        switch (state)
        {
            case BoardState.FILL_BOARD:
                GD.Print("BoardManager - _Process: Running State FILLBOARD");
                //Send a message to the FillBoardEvent
                FillBoardEvent fbei = new FillBoardEvent();
                //Get a refference to the board
                fbei.board = board;
                //Send the board size as a Vector2
                fbei.boardSize = new Vector2(boardWidth, boardHeight);
                //Send the event to the listeners
                fbei.FireEvent();
                //Afte the fill board state has benn run we switch to the wait state until usr iput changes the board status
                // State = BoardState.CHECK_MATCHES;
                State = BoardState.WAIT;
                break;
            case BoardState.CHECK_VOIDS:
                GD.Print("BoardManager - _Process: Running State CHECKVOIDS");
                //Send a message to the CheckEmptySlotsEvent
                CheckEmptySlotsEvent cesei = new CheckEmptySlotsEvent();
                //Get a refference to the board
                cesei.board = board;
                //Send the board size as a Vector2
                cesei.boardSize = new Vector2(boardWidth, boardHeight);
                //Send the event message
                cesei.FireEvent();
                break;
            case BoardState.CHECK_MATCHES:
                CheckTileMatchesEvent ctmei = new CheckTileMatchesEvent();
                ctmei.board = board;
                ctmei.FireEvent();
                break;
            case BoardState.WAIT:
                GD.Print("BoardManager - _Process: Running State WAIT");
                break;
            case BoardState.CLEAR_BOARD:
                GD.Print("BoardManager - _Process: Running State CLEARBOARD");
                //Clears the board of tiles
                //ClearBoard();
                //After clearing the board the wait state is run waiting for user input or something I hope
                State = BoardState.WAIT;
                break;
        }
    }
    //
    private void OnGetBoardEvent(GetBoardEvent gbei)
    {
        //Send the reference of the board ot the swap tile event
        gbei.board = board;
    }

    //Return the tile at the requested position
    private Node2D GetTile(Vector2 pos)
    {
        //Return the tile node at the requested position
        return board[(int)pos.x, (int)pos.y];
    }
    public void RemoveTileAt(Vector2 pos)
    {
        //Delete the tile object
        board[(int)pos.x, (int)pos.y].QueueFree();
        //Set the position in the list for the tile to null
        board[(int)pos.x, (int)pos.y] = null;
    }
    public void OnBoardStateChangeEvent(BoardStateChangeEvent bscei)
    {
        //Change the current state to the new state
        State = bscei.newState;
    }
    private void OnTileDestroyedEvent(TileDestroyedEvent tdei)
    {
        //When the tile is destroyed update the map
        RemoveTileAt(((Node2D)GD.InstanceFromId(tdei.tileID)).Position / 32);
        //Change board state
        BoardStateChangeEvent bscei = new BoardStateChangeEvent();
        bscei.newState = BoardState.CHECK_VOIDS;
        bscei.FireEvent();

    }
}
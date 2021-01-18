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

public class BoardStateManager : Node2D
{
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
        //When the boards state is changed from another class
        BoardStateChangeEvent.RegisterListener(OnBoardStateChangeEvent);
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
                InitBoardEvent ibei = new InitBoardEvent();
                //Send the event to the listeners
                ibei.FireEvent();
                //Afte the fill board state has benn run we switch to the wait state until usr iput changes the board status
                //State = BoardState.CHECK_MATCHES;
                State = BoardState.WAIT;
                break;
            case BoardState.CHECK_VOIDS:
                GD.Print("BoardManager - _Process: Running State CHECKVOIDS");
                //Send a message to the CheckEmptySlotsEvent
                CheckEmptySlotsEvent cesei = new CheckEmptySlotsEvent();
                //Send the event message
                cesei.FireEvent();
                break;
            case BoardState.CHECK_MATCHES:
                CheckTileMatchesEvent ctmei = new CheckTileMatchesEvent();
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
    public void OnBoardStateChangeEvent(BoardStateChangeEvent bscei)
    {
        //Change the current state to the new state
        State = bscei.newState;
    }
}
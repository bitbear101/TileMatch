using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public enum TileState
{
    CHECK,
    MOVE,
    WAIT
};
public class Tile : Node2D
{
    //The state of the tile
    TileState state;
    //A custom setter and getter for the state to send a message to all listeners when it is changed
    public TileState State
    {
        get { return state; }
        set
        {
            //When a new state is set we call the tile state change event to notify the listeners 
            TileStateChangeEvent tscei = new TileStateChangeEvent();
            //Set the value for the new state to send as a message
            tscei.newState = value;
            //Fire of the event
            tscei.FireEvent();
            //Set the new value of state
            state = value;
        }
    }
    //Run when the object is first instantiated
    public override void _Ready()
    {
        //We initialize the tile when it is created in game by setting the init state to true
        ChangeState(TileState.WAIT);
    }
    //Change the state of the tile
    public void ChangeState(TileState newState)
    {
        //Set the state to the new state
        State = newState;
    }
}

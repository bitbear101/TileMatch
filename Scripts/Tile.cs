using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
//The types of tiles
public enum TileType
{
    NONE,
    RED,
    BLUE,
    GREEN,
    PURPLE,
    YELLOW,
    GRAY,
    DARKGRAY
};
public class Tile
{
    //The position of the tile in the board
    Vector2 pos;
    //The state of the tile
    TileType type;
    //Set up the constructor for the Tile Class
    public Tile(Vector2 _pos)
    {
        //Set the tile position 
        pos = _pos;
        //Register the listener for the Set TIle Event
        SetTileTypeEvent.RegisterListener(OnSetTileTypeEvent);
    }
    //A custom setter and getter for the state to send a message to all listeners when it is changed
    public TileType Type
    {
        get { return type; }
        set
        {
            //If the new type is the same as the old we don't change the value or call the tile type change event
            if (type == value) return;
            //Set the new value of state
            type = value;
            //When a new state is set we call the tile state change event to notify the listeners 
            TileTypeChangedEvent ttcei = new TileTypeChangedEvent();
            //Set the value for the new state to send as a message
            ttcei.type = type;
            //Send the tiles position along the message as well    
            ttcei.pos = pos;
            //Fire of the event
            ttcei.FireEvent();
        }
    }
    //Change the type of the tile
    private void OnSetTileTypeEvent(SetTileTypeEvent sttei)
    {
        //If of the tile is not the same as the request for the tile type changes position we exit out of the method
        if (sttei.pos != pos) return;
        //Set the state to the new state
        Type = sttei.type;
    }
}

using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
//The types of tiles
public enum TileType
{
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
    //The constructor for the tile
    public Tile(TileType _type, Vector2 _pos)
    {
        //Set the type of the tile
        Type = _type;
        //Set the position of the tile in the array
        pos = _pos;
    }
    //The position of the tile in the board
    Vector2 pos;
    //The state of the tile
    TileType type;
    //A custom setter and getter for the state to send a message to all listeners when it is changed
    public TileType Type
    {
        get { return type; }
        set
        {
            //If the new type is the same as the old we don't change the value or call the tile type change event
            if(type == value) return;
            //When a new state is set we call the tile state change event to notify the listeners 
            TileTypeChangeEvent ttcei = new TileTypeChangeEvent();
            //Set the value for the new state to send as a message
            ttcei.type = value;
            //Send the tiles position along the message as well
            ttcei.pos = pos;
            //Fire of the event
            ttcei.FireEvent();
            //Set the new value of state
            type = value;
        }
    }
    //Change the type of the tile
    public void ChangeState(TileType _type)
    {
        //Set the state to the new state
        Type = _type;
    }
}

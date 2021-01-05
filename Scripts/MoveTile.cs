using Godot;
using System;
using EventCallback;

public class MoveTile : Node
{
    //The status of the state
    bool active = false;
    //The position of the tile in the array
    Vector2 boardPos;
    [Export]
    //The size of the tile in pixelsset to 32x32 by default
    int tileSize = 32;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the tile state change as a listener
        TileStateChangeEvent.RegisterListener(OnTileStateChangeEvent);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //If the state is not set as active then we exit out of the method wthout doing anything
        if (!active) return;
    }

    private void OnTileStateChangeEvent(TileStateChangeEvent tscei)
    {
        //If the tiles state has been changed and it is move tile we set active to true else it is false
        if (tscei.newState == TileState.MOVE)
        {
            active = true;
        }
        else
        {
            active = false;
        }
    }

    public void PlaceTileInWorld()
    {
        //Cast the node of the MoveTile scripts GetParent() to a Node2D and then set its position in the world 
        ((Node2D)GetParent()).Position = boardPos * tileSize;
    }
}

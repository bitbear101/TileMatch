using Godot;
using System;
using EventCallback;
public class CreateTile : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the listener for the create tile event
        CreateTileEvent.RegisterListener(OnCreateTileEvent);
    }

    private void OnCreateTileEvent(CreateTileEvent ctei)
    {
        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();
        //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
        TileType tempType = (TileType)rng.RandiRange(1, Enum.GetNames(typeof(TileType)).Length - 1);
        //Change the tile type to its new type
        SetTileTypeEvent sttei = new SetTileTypeEvent();
        sttei.pos = new Vector2(x, y);
        sttei.type = tempType;
        sttei.FireEvent();
    }
}

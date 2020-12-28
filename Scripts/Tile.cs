using Godot;
using System;
//The types of tiles
public enum TileType
{
    RED,
    BLUE,
    GREEN
};
public class Tile : Node2D
{ 
    TileType type;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();
        //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
        type = (TileType)rng.RandiRange(1, Enum.GetNames(typeof(TileType)).Length);
        
    }
}

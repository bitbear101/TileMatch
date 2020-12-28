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
        rng.Seed = 

    }



//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

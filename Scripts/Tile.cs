using Godot;
using System;
using EventCallback;
//The types of tiles
public enum TileType
{
    RED,
    BLUE,
    GREEN
};
public class Tile : Node2D
{
    //The sprite for the tile
    Sprite sprite;
    //The type of the tile
    TileType type;
    //When the tile type is changed we call the tile type change event
    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            //When the tile type is chosen send the message of the change out
            TileTypeChangeEvent ttcei = new TileTypeChangeEvent();
            //Set the tile type for the message
            ttcei.type = type;
            //Send the message
            ttcei.FireEvent();
            //Set the old type to the new type
            type = value;
        }
    }
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Assign the sprite to the sprite object on the object
        sprite = GetNode<Sprite>("Sprite");
        //Generate the tiles type
        GenerateTile();
        GD.Print("Tiles type: " + Type);
    }

    private void GenerateTile()
    {
        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();
        //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
        Type = (TileType)rng.RandiRange(0, Enum.GetNames(typeof(TileType)).Length - 1);
    }
}

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
public class InitTile : Node
{
    Sprite sprite;
    //A dictionary for the sprites for the tiles
    Dictionary<TileType, PackedScene> tileSprites = new Dictionary<TileType, PackedScene>();
    //The type of the tile
    TileType type;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

        //Regestir the get tile type event 
        GetTileTypeEvent.RegisterListener(OnGetTileTypeEvent);
        //Set up the tile sprites
        tileSprites.Add(TileType.BLUE, (ResourceLoader.Load("res://Scenes/TileSprites/BlueTile.tscn") as PackedScene));
        tileSprites.Add(TileType.GREEN, (ResourceLoader.Load("res://Scenes/TileSprites/GreenTile.tscn") as PackedScene));
        tileSprites.Add(TileType.RED, (ResourceLoader.Load("res://Scenes/TileSprites/RedTile.tscn") as PackedScene));
        tileSprites.Add(TileType.PURPLE, (ResourceLoader.Load("res://Scenes/TileSprites/PurpleTile.tscn") as PackedScene));
        tileSprites.Add(TileType.DARKGRAY, (ResourceLoader.Load("res://Scenes/TileSprites/DarkGrayTile.tscn") as PackedScene));
        tileSprites.Add(TileType.GRAY, (ResourceLoader.Load("res://Scenes/TileSprites/GrayTile.tscn") as PackedScene));
        tileSprites.Add(TileType.YELLOW, (ResourceLoader.Load("res://Scenes/TileSprites/YellowTile.tscn") as PackedScene));

        GenerateTile();
    }

    private void GenerateTile()
    {
        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();
        //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
        type = (TileType)rng.RandiRange(0, Enum.GetNames(typeof(TileType)).Length - 1);

        //If the type for the tile is in the dictionary
        if (tileSprites.ContainsKey(type))
        {
            //Set the sprite object to the instanced object ffrom the dictionary
            sprite = tileSprites[type].Instance() as Sprite;
            AddChild(sprite);
            //After instantiating the tile we move it to the correct place
            TileStateChangeEvent tscei = new TileStateChangeEvent();
            tscei.newState = TileState.MOVE;
            tscei.FireEvent();
        }
        else
        {
            GD.Print("Tile - No such tile type exists in dictionary, can not instantiate sprite for tile");
        }
    }
    private void OnGetTileTypeEvent(GetTileTypeEvent gttei)
    {
        if (GetParent().GetInstanceId() == gttei.id)
        {
            //Return the tile type
            gttei.type = type;
        }

    }
}

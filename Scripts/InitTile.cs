using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public class InitTile : Node2D
{
    Sprite sprite;
    //A dictionary for the sprites for the tiles
    Dictionary<TileType, PackedScene> tileSprites = new Dictionary<TileType, PackedScene>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Regestir the get tile type event 
        GetTileTypeEvent.RegisterListener(OnGetTileTypeEvent);
        //When the tiles type is changed
        TileTypeChangeEvent.RegisterListener(OnTileTypeChangeEvent);
        //Set up the tile sprites
        tileSprites.Add(TileType.BLUE, (ResourceLoader.Load("res://Scenes/TileSprites/BlueTile.tscn") as PackedScene));
        tileSprites.Add(TileType.GREEN, (ResourceLoader.Load("res://Scenes/TileSprites/GreenTile.tscn") as PackedScene));
        tileSprites.Add(TileType.RED, (ResourceLoader.Load("res://Scenes/TileSprites/RedTile.tscn") as PackedScene));
        tileSprites.Add(TileType.PURPLE, (ResourceLoader.Load("res://Scenes/TileSprites/PurpleTile.tscn") as PackedScene));
        tileSprites.Add(TileType.DARKGRAY, (ResourceLoader.Load("res://Scenes/TileSprites/DarkGrayTile.tscn") as PackedScene));
        tileSprites.Add(TileType.GRAY, (ResourceLoader.Load("res://Scenes/TileSprites/GrayTile.tscn") as PackedScene));
        tileSprites.Add(TileType.YELLOW, (ResourceLoader.Load("res://Scenes/TileSprites/YellowTile.tscn") as PackedScene));
    }

    private void OnTileTypeChangeEvent(TileTypeChangeEvent ttcei)
    {
        if(ttcei.pos != Position / 32) return;
        //If the type for the tile is in the dictionary
        if (tileSprites.ContainsKey(ttcei.type))
        {
            //Set the sprite object to the instanced object ffrom the dictionary
            sprite = tileSprites[ttcei.type].Instance() as Sprite;
            //Ad the node ot the scene as a cchile of this scripts owner
            AddChild(sprite);
        }
        else
        {
            GD.Print("Tile - No such tile type exists in dictionary, can not instantiate sprite for tile");
        }
        //Visually we can flip the tile to the new tile type so no vector movement has to happen
    }
    private void GenerateTile(TileType type)
    {
        //If the type for the tile is in the dictionary
        if (tileSprites.ContainsKey(type))
        {
            //Set the sprite object to the instanced object ffrom the dictionary
            sprite = tileSprites[type].Instance() as Sprite;
            AddChild(sprite);
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
            //gttei.type = type;
        }

    }
}

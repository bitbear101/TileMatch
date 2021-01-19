using Godot;
using System;
using System.Collections.Generic;
using EventCallback;

public class InitTile : Node2D
{
    //The sprite node for the tile
    Sprite sprite;
    //The size of the tiles - grab this from the tile 
    int tileSize = 32;
    //A dictionary for the sprites for the tiles
    Dictionary<TileType, PackedScene> tileSpritesScenes = new Dictionary<TileType, PackedScene>();
    //The dictionary for the instanced tile nodes
    Dictionary<TileType, Sprite> tileSprites = new Dictionary<TileType, Sprite>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //When the tiles type is changed
        TileTypeChangedEvent.RegisterListener(OnTileTypeChangeEvent);
        //Set up the tile sprites
        tileSpritesScenes.Add(TileType.BLUE, (ResourceLoader.Load("res://Scenes/TileSprites/BlueTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.GREEN, (ResourceLoader.Load("res://Scenes/TileSprites/GreenTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.RED, (ResourceLoader.Load("res://Scenes/TileSprites/RedTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.PURPLE, (ResourceLoader.Load("res://Scenes/TileSprites/PurpleTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.DARKGRAY, (ResourceLoader.Load("res://Scenes/TileSprites/DarkGrayTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.GRAY, (ResourceLoader.Load("res://Scenes/TileSprites/GrayTile.tscn") as PackedScene));
        tileSpritesScenes.Add(TileType.YELLOW, (ResourceLoader.Load("res://Scenes/TileSprites/YellowTile.tscn") as PackedScene));
        //Initialize the tile Node with the types of the sprites
        foreach (KeyValuePair<TileType, PackedScene> tileSprite in tileSpritesScenes)
        {
            //Set the sprite object to the instanced object ffrom the dictionary
            sprite = tileSprite.Value.Instance() as Sprite;
            //Add the tile to the dictionary for quick access using the tile types
            tileSprites.Add(tileSprite.Key, sprite);
            //Hide the sprites on creation
            sprite.Visible = false;
            //Ad the node ot the scene as a cchile of this scripts owner
            AddChild(sprite);
        }
    }

    private void OnTileTypeChangeEvent(TileTypeChangedEvent ttcei)
    {
        //If the positions of the tile and the tile object dont match we return out of the method
        if (ttcei.pos != ((Node2D)GetParent()).Position / tileSize) return;
        //If the tile type change is valid we make all the sprites on that tile object invisible before setting the new one to visible
        TurnOffAllSprites();
        //If the type for the tile is in the dictionary
        if (tileSpritesScenes.ContainsKey(ttcei.type))
        {
            //Set the coresponding sprite for the tile node to visible
            tileSprites[ttcei.type].Visible = true;
        }
        else
        {
            GD.Print("Tile - No such tile type exists in dictionary, can not instantiate sprite for tile");
        }
        //Visually we can flip the tile to the new tile type so no vector movement has to happen
    }
    private void TurnOffAllSprites()
    {
        //Interate through all the sprites on the tile node
        foreach (KeyValuePair<TileType, Sprite> tileSprite in tileSprites)
        {
            //Set the current sprites visible to false
            tileSprite.Value.Visible = false;
        }
    }
}

using Godot;
using System;
using EventCallback;
public class FillBoard : Node
{
    //The scene for the tile object
    PackedScene tileScene;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
        //Register the fill board messanger event
        FillBoardEvent.RegisterListener(OnFillBoardEvent);
    }
    private void OnFillBoardEvent(FillBoardEvent fbei)
    {
        GD.Print("BoardManager - FillBoard: Running");
        //Loop through the board
        for (int x = 0; x < fbei.boardSize.x; x++)
        {
            for (int y = 0; y < fbei.boardSize.y; y++)
            {
                //Instantiate the tilscene and set the boards node 
                fbei.board[x, y] = ((Node2D)tileScene.Instance());
                //Set the tiles position in the world
                fbei.board[x, y].Position = new Vector2(x * 32, y * 32);
                //GD.Print("BoardManager - FillBoard: boardTiles[x, y].Position = " + boardTiles[x, y].Position);
                AddChild(fbei.board[x, y]);
            }
        }
        GD.Print("BoardManager - FillBoard: Done");
    }
}
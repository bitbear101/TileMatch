using Godot;
using System;
using EventCallback;
//NOTE -------
//NOTE NOTE Responsible for rendering all the tiles on the board, when changes have been made this method is responible to pdate all the tile visuals
//NOTE -------
public class NodesBoard : Node2D
{
    //The board of nodes for the tiles
    Node2D[,] nodeBoardTiles;
    //The scene for the tile object
    PackedScene tileScene;
    //The size of the tiles
    int tileSize = 32;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
        //Register as a listener for the create tile event
        CreateTileEvent.RegisterListener(OnCreateTileEvent);
        //Init the board tile nodes
        InitBoard();
    }
    //Create the tile objects
    private void InitBoard()
    {
        //Get the boards size in tiles
        GetBoardEvent gbei = new GetBoardEvent();
        //Fire the event to get the board
        gbei.FireEvent();
        //Set up the array for the node board tiles
        nodeBoardTiles = new Node2D[gbei.board.GetLength(0), gbei.board.GetLength(1)];
    }

    //Creates the tile
    private void OnCreateTileEvent(CreateTileEvent ctei)
    {
        //Instance the object from the packes scene
        nodeBoardTiles[(int)ctei.pos.x, (int)ctei.pos.y] = (Node2D)tileScene.Instance();
        //Set the Node2Ds position opropriate to the worlds coordinates
        nodeBoardTiles[(int)ctei.pos.x, (int)ctei.pos.y].Position = new Vector2((int)ctei.pos.x * tileSize, (int)ctei.pos.y * tileSize);
        //Add the Node2D object ot hte scene as a child
        AddChild(nodeBoardTiles[(int)ctei.pos.x, (int)ctei.pos.y]);
    }
    //Updates the tiles visuals
    private void OnUpdateTileEvent()
    {

    }
}

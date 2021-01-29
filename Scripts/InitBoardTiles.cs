using Godot;
using System;
using EventCallback;
//NOTE -------
//NOTE NOTE Responsible for rendering all the tiles on the board, when changes have been made this method is responible to pdate all the tile visuals
//NOTE -------
public class InitBoardTiles : Node2D
{
    //The board of nodes for the tiles
    Node2D[,] nodeBoardTiles;
    //The scene for the tile object
    PackedScene tileScene;
    //The size of the tiles
    [Export]
    int tileSize = 32;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
        //Initialize the objects tiles for the board
        InitBoardTilesEvent.RegisterListener(OnInitBoardTilesEvent);
        //Register the get tile size as listener
        GetTileSizeEvent.RegisterListener(OnGetTileSizeEvent);
    }
    //Create the tile objects
    private void OnInitBoardTilesEvent(InitBoardTilesEvent ibtei)
    {
        GD.Print("InitBoardTiles - OnInitBoardTilesEvent: Called");
        //Fire od the message event to get the size of the board
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        gbsei.FireEvent();
        //Set up the array for the node board tiles
        nodeBoardTiles = new Node2D[gbsei.boardSizeX, gbsei.boardSizeY];

        for (int y = 0; y < gbsei.boardSizeY; y++)
        {
            for (int x = 0; x < gbsei.boardSizeX; x++)
            {
                //Instance the object from the packes scene
                nodeBoardTiles[x, y] = (Node2D)tileScene.Instance();
                //Set the Node2Ds position opropriate to the worlds coordinates
                nodeBoardTiles[x, y].Position = new Vector2(x * tileSize, y * tileSize);
                //Add the Node2D object ot hte scene as a child
                AddChild(nodeBoardTiles[x, y]);
                //GD.Print("VisualBoard - InitBoard: nodeBoardTiles[x, y] ID = " + nodeBoardTiles[x, y].GetInstanceId());
            }
        }
    }

    private void OnGetTileSizeEvent(GetTileSizeEvent gtsei)
    {
        //Return the tile size to the message sender
        gtsei.size = tileSize;
    }
}

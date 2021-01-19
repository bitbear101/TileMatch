using Godot;
using System;
using EventCallback;
//NOTE -------
//NOTE NOTE Responsible for rendering all the tiles on the board, when changes have been made this method is responible to pdate all the tile visuals
//NOTE -------
public class VisualBoard : Node2D
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
        //Register the InitVisualBoardEvent listener in this class
        InitVisualBoardEvent.RegisterListener(OnInitVisualBoardEvent);
    }
    //Create the tile objects
    private void OnInitVisualBoardEvent(InitVisualBoardEvent ivbei)
    {
        GD.Print("ViualBoard - OnInitVisualBoardEvent: Called");
        //Send a message requesting the tile board to get its size later
        GetBoardEvent gbei = new GetBoardEvent();
        gbei.FireEvent();
        //Set up the array for the node board tiles
        nodeBoardTiles = new Node2D[gbei.board.GetLength(0), gbei.board.GetLength(1)];

        for (int y = 0; y < nodeBoardTiles.GetLength(1); y++)
        {
            for (int x = 0; x < nodeBoardTiles.GetLength(0); x++)
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
    //Updates the tiles visuals
    private void OnUpdateTileEvent()
    {

    }
}

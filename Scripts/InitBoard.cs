using Godot;
using System;
using EventCallback;
public class InitBoard : Node
{
    //The scene for the tile object
    PackedScene tileScene;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
        //Register the fill board messanger event
        InitBoardEvent.RegisterListener(OnInitBoardEvent);
    }
    private void OnInitBoardEvent(InitBoardEvent ibei)
    {
        GD.Print("InitBoard - OnInitBoardEvent: Running");

        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();

        GetBoardEvent gbei = new GetBoardEvent();
        //Fire the event to get the board
        gbei.FireEvent();
        //Loop through the board
        for (int y = 0; y < gbei.board.GetLength(1); y++)
        {
            for (int x = 0; x < gbei.board.GetLength(0); x++)
            {
                //The temp tile type the check
                TileType tempType;
                //The do loop for the type creation of the tile
                do
                {
                    //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
                    tempType = (TileType)rng.RandiRange(0, Enum.GetNames(typeof(TileType)).Length - 1);
                } while (CheckMatches(gbei.board, x, y, tempType));
                //Instantiate the tilscene and set the boards node 
                gbei.board[x, y] = new Tile(tempType, new Vector2(x, y));
            }
        }
        GD.Print("InitBoard - OnInitBoardEvent: Done");
    }
    //Check if there are matches to the top and left of the injected position
    private bool CheckMatches(Tile[,] board, int tilePosX, int tilePosY, TileType type)
    {
        //Set the positions to check
        int checkPosX = tilePosX - 1;
        int checkPosY = tilePosY - 1;
        //The bool that will be set to true if there are any matches
        bool matches = false;
        //If the position we want to check is not out of bounds with the boards size
        if (checkPosX > 0)
        {
            //Check if the tile types are the same, if so set matches to true
            if (board[checkPosX, tilePosY].Type == type) matches = true;
        }
        if (checkPosY > 0)
        {
            //Check if the tile types are the same, if so set matches to true
            if (board[tilePosX, checkPosY].Type == type) matches = true;
        }
        //Return if there was any matches
        return matches;
    }
}
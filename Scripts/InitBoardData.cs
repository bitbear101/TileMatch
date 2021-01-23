using Godot;
using System;
using EventCallback;
public class InitBoardData : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the fill board messanger event
        InitBoardDataEvent.RegisterListener(OnInitBoardEvent);
    }

    private void OnInitBoardEvent(InitBoardDataEvent ibei)
    {
        GD.Print("InitBoardData - OnInitBoardEvent: Running");
        //Instantiate the event message sender for the get board size event
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        gbsei.FireEvent();


        //Set up the random number generator
        RandomNumberGenerator rng = new RandomNumberGenerator();
        //Randomize the random number generators seed
        rng.Randomize();

        //Loop through the board
        for (int y = 0; y < gbsei.boardSizeY; y++)
        {
            for (int x = 0; x < gbsei.boardSizeX; x++)
            {
                //The temp tile type the check
                TileType tempType;
                //The do loop for the type creation of the tile
                do
                {
                    //Generate a tile based on the mount of entries in the enum, so the enum size can change as log as custom numbering is not used
                    tempType = (TileType)rng.RandiRange(1, Enum.GetNames(typeof(TileType)).Length - 1);
                } while (CheckMatches(new Vector2(x, y), tempType));
                //Instantiate the event message sender fir the set tile type
                SetTileTypeEvent sttei = new SetTileTypeEvent();
                //Change the tile type to its new type
                sttei.EventName = "SetTileTypeEvent";
                sttei.pos = new Vector2(x, y);
                sttei.type = tempType;
                sttei.FireEvent();
            }
        }
        GD.Print("InitBoardData - OnInitBoardEvent: Done");
    }

    //Check if there are matches to the top and left of the injected position
    private bool CheckMatches(Vector2 tilePos, TileType type)
    {
        //Set the positions to check
        Vector2 checkPos = tilePos + Vector2.NegOne;
        //The bool that will be set to true if there are any matches
        bool matches = false;
        //If the position we want to check is not out of bounds with the boards size
        if (checkPos.x > 0)
        {
            //Instantiate the the tile type event callback, We set up the event handler here as not
            GetTileTypeEvent gttei = new GetTileTypeEvent();
            gttei.EventName = "GetTileTypeEvent";
            gttei.pos = new Vector2(checkPos.x, tilePos.y);
            gttei.FireEvent();
            //Check if the tile types are the same, if so set matches to true
            if (gttei.type == type) matches = true;
        }
        if (checkPos.y > 0)
        {
            //Instantiate the the tile type event callback, We set up the event handler here as not
            GetTileTypeEvent gttei = new GetTileTypeEvent();
            gttei.EventName = "GetTileTypeEvent";
            gttei.pos = new Vector2(tilePos.x, checkPos.y);
            gttei.FireEvent();
            //Check if the tile types are the same, if so set matches to true
            if (gttei.type == type) matches = true;
        }
        //Return if there was any matches
        return matches;
    }
}
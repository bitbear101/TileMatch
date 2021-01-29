using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class CheckTileMatches : Node
{
    //The list of vertical matches
    List<Vector2> vMatches = new List<Vector2>();
    //The list of horizontal matches
    List<Vector2> hMatches = new List<Vector2>();

    //The vertical and horizontal lists combined that will be sent for removal, I do it this way becuase there could be doubles 
    List<Vector2> tilesToRemove = new List<Vector2>();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the Check tile matches event listener
        CheckTileMatchesEvent.RegisterListener(OnCheckTileMatchesEvent);
    }
    //Check for matching neighbouring tiles
    private void OnCheckTileMatchesEvent(CheckTileMatchesEvent ctmei)
    {
        GD.Print("CheckTileMatches - OnCheckTileMatchesEvent: Running");
        //Add the first tile to the open lists for the horizontal and vertical
        vMatches.Add(ctmei.tilePos);
        hMatches.Add(ctmei.tilePos);
        //The original tiles type
        //Register the event callback
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //The postion of the original tiles type
        gttei.pos = ctmei.tilePos;
        gttei.FireEvent();
        //Set the original tiles type
        TileType originTileType = gttei.type;
        //Check the horizontal tiles 
        CheckHTiles(originTileType, ctmei.tilePos);
        CheckVTiles(originTileType, ctmei.tilePos);

        if (vMatches.Count > 2)
        {
            ctmei.matches = true;
        }
        else if (hMatches.Count > 2)
        {
            ctmei.matches = true;
        }

    }

    private void CheckHTiles(TileType origin, Vector2 tilePos)
    {
        //Register the event callback
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //The postion of the original tiles shifted one position to the left
        gttei.pos = tilePos + new Vector2(-1, 0);
        gttei.FireEvent();

        //Check the vertical 
        if (gttei.type == origin)
        {
            //Register the event callback
            GetTileTypeEvent gtteiHM = new GetTileTypeEvent();
            hMatches.Add((tilePos + new Vector2(-1, 0)));
            //The postion of the original tiles shifted one position to the left
            gtteiHM.pos = tilePos + new Vector2(-2, 0);
            gtteiHM.FireEvent();
            //Check the vertical 
            if (gtteiHM.type == origin)
            {
                hMatches.Add((tilePos + new Vector2(-2, 0)));
            }
        }
        GetTileTypeEvent gtteitp = new GetTileTypeEvent();
        //The postion of the original tiles shifted one position to the left
        gtteitp.pos = tilePos + new Vector2(1, 0);
        gtteitp.FireEvent();

        //Check the vertical 
        if (gtteitp.type == origin)
        {
            GetTileTypeEvent gtteintp = new GetTileTypeEvent();
            hMatches.Add((tilePos + new Vector2(1, 0)));
            //The postion of the original tiles shifted one position to the left
            gtteintp.pos = tilePos + new Vector2(2, 0);
            gtteintp.FireEvent();
            //Check the vertical 
            if (gtteintp.type == origin)
            {
                hMatches.Add((tilePos + new Vector2(2, 0)));
            }
        }
    }
    private void CheckVTiles(TileType origin, Vector2 tilePos)
    {
        //Register the event callback
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //The postion of the original tiles shifted one position to the left
        gttei.pos = tilePos + new Vector2(0, -1);
        gttei.FireEvent();

        //Check the vertical 
        if (gttei.type == origin)
        {
            GetTileTypeEvent gtteiVM = new GetTileTypeEvent();
            vMatches.Add((tilePos + new Vector2(0, -1)));
            //The postion of the original tiles shifted one position to the left
            gtteiVM.pos = tilePos + new Vector2(0, -2);
            gtteiVM.FireEvent();
            //Check the vertical 
            if (gtteiVM.type == origin)
            {
                vMatches.Add((tilePos + new Vector2(0, -2)));
            }
        }
        GetTileTypeEvent gtteitp = new GetTileTypeEvent();
        //The postion of the original tiles shifted one position to the left
        gtteitp.pos = tilePos + new Vector2(0, 1);
        gtteitp.FireEvent();

        //Check the vertical 
        if (gtteitp.type == origin)
        {
            GetTileTypeEvent gtteintp = new GetTileTypeEvent();
            vMatches.Add((tilePos + new Vector2(0, 1)));
            //The postion of the original tiles shifted one position to the left
            gtteintp.pos = tilePos + new Vector2(0, 2);
            gtteintp.FireEvent();
            //Check the vertical 
            if (gtteintp.type == origin)
            {
                vMatches.Add((tilePos + new Vector2(0, 2)));
            }
        }
    }
}

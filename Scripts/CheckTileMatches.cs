using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class CheckTileMatches : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the Check tile matches event listener
        CheckTileMatchesEvent.RegisterListener(OnCheckTileMatchesEvent);
    }

    //Check for matching neighbouring tiles
    private void OnCheckTileMatchesEvent(CheckTileMatchesEvent ctmei)
    {
        /*        
        //Checking tile matches seperately on the x and then the y axis makes sure we only check
        //for vertical and horizontal matches only
        //Check all the x position tiles first
        for (int x = (int)tile.Position.x - 1; x < (int)tile.Position.x + 1; x++)
        {
            //Make sure the tile is not checking against itself by making sure the position is not the same
            if (tile.Position != x)
            {
                //Get the script on the node object and then grab the tile types from that for a comparison
                if (((Tile)tile).type == ((Tile)GetTile(new Vector2(x, y))).type)
                {

                }
            }
        }
        //Check all the y position tiles 
        for (int y = (int)tile.Position.y - 1; y < (int)tile.Position.y + 1; y++)
        {
            if (tile.Position.y != y)
            {
                //Get the script on the node object and then grab the tile types from that for a comparison
                if (((Tile)tile).type == ((Tile)GetTile(new Vector2(x, y))).type)
                {

                }
            }
        }
        */
    }


}

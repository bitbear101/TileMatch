using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class CheckTileMatches : Node
{
    Node2D[,] board;
    Vector2 boardSize;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the Check tile matches event listener
        CheckTileMatchesEvent.RegisterListener(OnCheckTileMatchesEvent);
    }
    //Check for matching neighbouring tiles
    private void OnCheckTileMatchesEvent(CheckTileMatchesEvent ctmei)
    {
        board = ctmei.board;
        boardSize = ctmei.boardSize;

        //The lists for the 3 or more matching tiles in repective rows
        List<Vector2> verticalMatches = new List<Vector2>();
        List<Vector2> horizontalMatches = new List<Vector2>();
        //The vertical and horizontal lists combined that will be sent for removal, I do it this way becuase there could be doubles 
        List<Vector2> tilesToRemove = new List<Vector2>();

        //Run through all the 
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {

            }
        }
    }

    private Node2D GetTile(Vector2 pos)
    {
        //Return the tile node at the requested position
        return boardTiles[(int)pos.x, (int)pos.y];
    }

    private TileType GetTileType(Node2D tile)
    {
        //Send a message to request the tiles type
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        gttei.id = tile.GetInstanceId();
        gttei.FireEvent();
        return gttei.type;
    }

    private void MatchBoard()
    {

    }

    private void MatchTiles(Node2D tile)
    {

 //Checking tile matches seperately on the x and then the y axis makes sure we only check
        //for vertical and horizontal matches only
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
    }


}

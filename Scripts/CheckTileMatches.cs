using Godot;
using System;
using System.Collections.Generic;
using EventCallback;
public class CheckTileMatches : Node
{
    /*
    //The reference to the main board 
    Node2D[,] board;
    //The reference to the size of the map
    Vector2 boardSize;
    //The list for tiles that still need to be checked
    List<Vector2> openList = new List<Vector2>();
    //The list of tiles that have already been checked but are not a match
    List<Vector2> closedList = new List<Vector2>();
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
        //Set the board reference to the current board
        board = ctmei.board;
        //Set the size of the board
        boardSize = ctmei.boardSize;
        //Add the first tile to the open list
        openList.Add(ctmei.tilePos);
        //Loop through the list until the open list is empty
        while (openList.Count > 0)
        {
            CheckTiles(ctmei.tilePos);
        }
    }
    private Node2D GetTile(Vector2 pos)
    {
        //Return the tile node at the requested position
        return board[(int)pos.x, (int)pos.y];
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
        //Run through all the  tiles on the board
        for (int y = 0; y < boardSize.y; y++)
        {
            for (int x = 0; x < boardSize.x; x++)
            {

            }
        }
    }
    private void CheckTiles(Vector2 tilePos)
    {
        //Check all the y position tiles 
        for (int y = (int)tilePos.y - 1; y < (int)tilePos.y + 1; y++)
        {
            //If the tile we are checking is not the same tile that was injected and if the tile is not in the closed list 
            if (tilePos.y != y && !closedList.Contains(new Vector2(tilePos.x, y)) && !openList.Contains(new Vector2(tilePos.x, y)))
            {
                //if the tile types of the input tile and the tile we are checking are the same
                if (GetTileType(GetTile(tilePos)) == (GetTileType(GetTile(new Vector2(tilePos.x, y)))))
                {
                    //If the tile type is the same 
                    openList.Add(new Vector2(tilePos.x, y));
                    vMatches.Add(new Vector2(tilePos.x, y));
                }
            }
        }
        //Checking tile matches seperately on the x and then the y axis makes sure we only check
        //for vertical and horizontal matches only
        for (int x = (int)tilePos.x - 1; x < (int)tilePos.x + 1; x++)
        {
            //Make sure the tile is not checking against itself by making sure the position is not the same
            if (tilePos.x != x && !closedList.Contains(new Vector2(x, tilePos.y)) && !openList.Contains(new Vector2(x, tilePos.y)))
            {
                //Get the script on the node object and then grab the tile types from that for a comparison
                if (GetTileType(GetTile(tilePos)) == (GetTileType(GetTile(new Vector2(x, tilePos.y)))))
                {
                    //If the horizontal tiles are of the same type they are loaded into the open list to check thier neighbours as well
                    openList.Add(new Vector2(x, tilePos.y));
                    hMatches.Add(new Vector2(x, tilePos.y));
                }
            }
        }
        //If the closed list does not contain the tile being checked yet then add it to the list 
        if (!closedList.Contains(tilePos)) closedList.Add(tilePos);
        //Remove the checked tile from the open list
        //Get the index position in the list of the tile we want to remove from the list
        int listPos = openList.FindIndex(Vector2 => Vector2 == tilePos);
        //Remove the tile from the list
        openList.RemoveAt(listPos);
    }
    */
}

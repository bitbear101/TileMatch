using Godot;
using System;
using EventCallback;
public class Board : Node
{
    //The boards width and height
    const int boardWidth = 9, boardHeight = 9;
    //Create the for the board
    Tile[,] board = new Tile[boardWidth, boardHeight];

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the listener for hte get tile type event
        GetTileTypeEvent.RegisterListener(OnGetTileTypeEvent);
        //The listener for the get boards size
        GetBoardSizeEvent.RegisterListener(OnGetBoardSizeEvent);
        //Initialize the tile board with empty tiles
        CreateBoard();
    }

    private void CreateBoard()
    {
        //Create the empty tiles for te board to populate the array
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                //Create a new tile in the board
                board[x, y] = new Tile(new Vector2(x, y));
            }
        }
    }

    //Returns the type of the tile requested
    private void OnGetTileTypeEvent(GetTileTypeEvent gttei)
    {
        //Get the tile type at the position of the tile
        gttei.type = board[(int)gttei.pos.x, (int)gttei.pos.y].Type;
    }
    //Returns the size of the board
    private void OnGetBoardSizeEvent(GetBoardSizeEvent gbsei)
    {
        //Return the boards size in through the messanger
        gbsei.boardSizeX = boardWidth;
        gbsei.boardSizeY = boardHeight;
    }
}

using Godot;
using System;
using EventCallback;
public class SwapTiles : Node
{
    //The refference to the board
    Node2D[,] board;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the swap tile event listener
        SwapTilesEvent.RegisterListener(OnSwapTileEvent);
    }

    private void OnSwapTileEvent(SwapTilesEvent stei)
    {
        //Get the reference to the board
        GetBoardEvent gbei = new GetBoardEvent();
        //Fire the message that returns the board
        gbei.FireEvent();
        //Set the board to the board retrieved by the event callback
        board = gbei.board;

        GD.Print("SwapTiles - OnSwapTileEvent: Running");
        //The direction for the drag
        Vector2 dir;
        //Get the direction of the drag
        dir = GetDirection(stei.dragStartPos, stei.dragEndPos);

        //If the direction for the swipe is negative one then the swipe is invalid
        if (dir != Vector2.NegOne)
        {
            GD.Print("SwapTile - OnSwapTileEvent: dir = " + dir);
            //Get the tile object in the world
            Node2D tile = (Node2D)GD.InstanceFromId(stei.tileID);
            GD.Print("SwapTile - OnSwapTileEvent: tile.Position = " + tile.Name);
            //Get the tiles position in the board
            Vector2 tileBoardPos = tile.Position / 32;
            GD.Print("SwapTile - OnSwapTileEvent: tileBoardPos = " + tileBoardPos);
            //The neighbouring tiles position in the board
            Vector2 neighbourBoardPos = tileBoardPos + dir;

            if (!WithinBounds(neighbourBoardPos)) return;
            GD.Print("SwapTile - OnSwapTileEvent: neighbourBoardPos = " + neighbourBoardPos);
            //The ttile object for the neighbouring tile
            Node2D neighbourTile = gbei.board[(int)neighbourBoardPos.x, (int)neighbourBoardPos.y];
            GD.Print("SwapTile - OnSwapTileEvent: neighbourTile.Position = " + neighbourTile.Name);
        }
        else
        {
            //Error message to throw for the incorrect swipe
            GD.Print("SwapTiles - OnSwapTileEvent: Swipe direction was invalid for some reason");
        }
    }
    /*
        private Node2D GetNeighbour()
        {
            return ;
        }
    */
    private Vector2 GetDirection(Vector2 start, Vector2 end)
    {
        //Get the direction of drag
        Vector2 dir = end - start;
        //The drag diretion to return
        Vector2 dragDirection = Vector2.NegOne;
        //Check if the x movement is bigger than the y movement
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            //If the x value is positive
            if (dir.x > 0)
            {
                dragDirection = Vector2.Right;
            }
            //If the x value was negative
            else
            {
                dragDirection = Vector2.Left;
            }
        }
        //If the drag was more on the y axis
        else
        {
            //If the y value is positive
            if (dir.y > 0)
            {
                dragDirection = Vector2.Down;
            }
            else
            {
                dragDirection = Vector2.Up;
            }
        }
        //Return the direction
        return dragDirection;
    }

    private bool WithinBounds(Vector2 pos)
    {
        //Set the in bounds to true for the upcoming checks
        bool inBounds = true;
        //Check if the position being passed is within the bounds of the board array
        //If the new position larger than the last entry in the array on the x positions
        if (board.GetLength(0) -1 < pos.x) inBounds = false;
        //Id the new position less than 0, the first position of the array in the x positions
        if (0 > pos.x) inBounds = false;
        //If the new position larger than the last entry in the array in the y positions
        if (board.GetLength(1) -1 < pos.y) inBounds = false;
        //Id the new position less than 0, the first position of the array in the y positions
        if (0 > pos.y) inBounds = false;

        GD.Print("SpawnTile - WithinBounds: inBounds = " + inBounds);
        return inBounds;
    }
}

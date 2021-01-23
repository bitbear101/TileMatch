using Godot;
using System;
using EventCallback;
public class SwapTiles : Node
{
    int boardWidth, boardHeight;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the swap tile event listener
        SwapTilesEvent.RegisterListener(OnSwapTileEvent);
    }

    private void OnSwapTileEvent(SwapTilesEvent stei)
    {
        //Get the size to the board using the event messaging system
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        //Fire the message that returns the board
        gbsei.FireEvent();
        boardWidth = gbsei.boardSizeX;
        boardHeight = gbsei.boardSizeY;
        //The Event callback messaging to get the tile type from the injected position
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //The set tile type event callback 
        SetTileTypeEvent sttei = new SetTileTypeEvent();
        //Send the event message to get hte tiles sixe in pixels
        GetTileSizeEvent gtsei = new GetTileSizeEvent();
        gtsei.FireEvent();

        GD.Print("SwapTiles - OnSwapTileEvent: Running");
        //The direction for the drag
        Vector2 dir;
        //Get the direction of the drag
        dir = GetDirection(stei.dragStartPos, stei.dragEndPos);

        //If the direction for the swipe is negative one then the swipe is invalid
        if (dir != Vector2.NegOne)
        {
            //Get the tiles position in the board
            Vector2 originTilePos = stei.dragStartPos / gtsei.size;
            //The neighbouring tiles position in the board
            Vector2 neighbourTilePos = originTilePos + dir;
            //Check if the new neighbours position is within the boards boundries
            if (!WithinBounds(neighbourTilePos)) return;
            //The ttile object for the neighbouring tile
            //Node2D neighbourTile = gbei.board[(int)neighbourBoardPos.x, (int)neighbourBoardPos.y];

            //Get the tile type for the origin tile from the event callback system
            gttei.pos = originTilePos;
            gttei.FireEvent();
            //A temporary storage for the tile type of the origin tile
            TileType originTileType = gttei.type;
            //Get the neighbouring tile type
            gttei.pos = neighbourTilePos;
            gttei.FireEvent();
            TileType neighbourTileType = gttei.type;
            //Set the neighbour to the original tile type
            sttei.pos = neighbourTilePos;
            sttei.type = originTileType;
            sttei.FireEvent();
            //Set the origin tile type to the neighnours
            sttei.pos = originTilePos;
            sttei.type = neighbourTileType;
            sttei.FireEvent();

            //TO DO: If the new tile positions have matche we keep the change and if not we reverse the tiles
            bool matched = true;

            if (!matched)
            {
                //Set the neighbour to the original tile type
                sttei.pos = neighbourTilePos;
                sttei.type = neighbourTileType;
                sttei.FireEvent();
                //Set the origin tile type to the neighnours
                sttei.pos = originTilePos;
                sttei.type = originTileType;
                sttei.FireEvent();
            }

            //Call the board state schange 
            //BoardStateChangeEvent bscei = new BoardStateChangeEvent();
        }
        else
        {
            //Error message to throw for the incorrect swipe
            GD.Print("SwapTiles - OnSwapTileEvent: Swipe direction was invalid for some reason");
        }
    }
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
        if (boardWidth < pos.x) inBounds = false;
        //Id the new position less than 0, the first position of the array in the x positions
        if (0 > pos.x) inBounds = false;
        //If the new position larger than the last entry in the array in the y positions
        if (boardHeight < pos.y) inBounds = false;
        //Id the new position less than 0, the first position of the array in the y positions
        if (0 > pos.y) inBounds = false;
        //Return the resul for the in bound check
        return inBounds;
    }
}

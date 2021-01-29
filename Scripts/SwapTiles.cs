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
        GD.Print("SwapTiles - OnSwapTileEvent: Running");

        //Get the size to the board using the event messaging system
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        //Fire the message that returns the board
        gbsei.FireEvent();
        boardWidth = gbsei.boardSizeX;
        boardHeight = gbsei.boardSizeY;
        GD.Print("SwapTiles - OnSwapTileEvent: Running");

        GD.Print("SwapTiles - OnSwapTileEvent: boardWidth = " + boardWidth);
        GD.Print("SwapTiles - OnSwapTileEvent: boardHeight = " + boardHeight);


        //The Event callback messaging to get the tile type from the injected position
        GetTileTypeEvent gttei = new GetTileTypeEvent();
        //The set tile type event callback 
        SetTileTypeEvent sttei = new SetTileTypeEvent();
        //Send the event message to get hte tiles sixe in pixels
        GetTileSizeEvent gtsei = new GetTileSizeEvent();
        gtsei.FireEvent();
        //The direction for the drag
        Vector2 dir;
        //Get the direction of the drag
        dir = GetDirection(stei.dragStartPos, stei.dragEndPos);

        //If the direction for the swipe is negative one then the swipe is invalid
        if (dir != Vector2.NegOne)
        {
            GD.Print("SwapTiles - OnSwapTileEvent: originTilePos before = " + stei.dragStartPos);

            //Get the tiles position in the board
            Vector2 originTilePos = new Vector2((int)(stei.dragStartPos.x - 500) / gtsei.size, ((int)stei.dragStartPos.y - 300) / gtsei.size);
            GD.Print("SwapTiles - OnSwapTileEvent: originTilePos = " + originTilePos);

            //The neighbouring tiles position in the board
            Vector2 neighbourTilePos = originTilePos + dir;
            //Check if the new neighbours position is within the boards boundries
            if (!WithinBounds(neighbourTilePos)) return;
            GD.Print("SwapTiles - OnSwapTileEvent: dir is valid ");
            //Get the tile type for the origin tile from the event callback system
            gttei.pos = originTilePos;
            gttei.FireEvent();
            //A temporary storage for the tile type of the origin tile
            TileType originTileType = gttei.type;
            //Get the neighbouring tile type
            GetTileTypeEvent gttein = new GetTileTypeEvent();
            gttein.pos = neighbourTilePos;
            gttein.FireEvent();
            TileType neighbourTileType = gttein.type;
            //Set the neighbour to the original tile type
            sttei.pos = neighbourTilePos;
            sttei.type = originTileType;
            sttei.FireEvent();
            //Set the origin tile type to the neighnours
            SetTileTypeEvent stteio = new SetTileTypeEvent();
            stteio.pos = originTilePos;
            stteio.type = neighbourTileType;
            stteio.FireEvent();
            //Te main bool to state if there where any tile matches from the two tiles thaat changed position
            bool matches = false;
            //Check the tiles that changed position 
            CheckTileMatchesEvent ctmei = new CheckTileMatchesEvent();
            ctmei.tilePos = originTilePos;
            ctmei.FireEvent();
            //If there where matching tiles we set the matches bool to true
            if (ctmei.matches)
            {
                matches = ctmei.matches;
            }
CheckTileMatchesEvent ctmei2 = new CheckTileMatchesEvent();
            //Check the neighbouring tile thatwas moves for matches
            ctmei2.tilePos = neighbourTilePos;
            ctmei2.FireEvent();
            //If there where matching tiles we set the matches bool to true
            if (ctmei2.matches)
            {
                matches = ctmei2.matches;
            }
            //If there where no matches we reset the movement
            if (!matches)
            {
                GD.Print("SwapTiles - OnSwapTileEvent: No matches found");
                //Set the neighbour to the original tile type
                sttei.pos = neighbourTilePos;
                sttei.type = neighbourTileType;
                sttei.FireEvent();
                //Set the origin tile type to the neighnours
                sttei.pos = originTilePos;
                sttei.type = originTileType;
                sttei.FireEvent();
            }
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
        int boardBounds = boardWidth * boardHeight;
        //Check if the position being passed is within the bounds of the board array
        //If the new position larger than the last entry in the array on the x positions
        if (boardBounds < pos.x) inBounds = false;
        //Id the new position less than 0, the first position of the array in the x positions
        if (0 > pos.x) inBounds = false;
        //If the new position larger than the last entry in the array in the y positions
        if (boardBounds < pos.y) inBounds = false;
        //Id the new position less than 0, the first position of the array in the y positions
        if (0 > pos.y) inBounds = false;
        //Return the result for the in bound check
        return inBounds;
    }
}

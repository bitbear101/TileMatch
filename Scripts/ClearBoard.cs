using Godot;
using System;
using EventCallback;
public class ClearBoard : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Regester this listener to the clear board event
        ClearBoardEvent.RegisterListener(OnClearBoardEvent);
    }
    private void OnClearBoardEvent(ClearBoardEvent cbei)
    {
        //Get the boards size in tiles not pixels
        GetBoardSizeEvent gbsei = new GetBoardSizeEvent();
        gbsei.FireEvent();
        //Loop through the entire board        
        for (int y = 0; y < gbsei.boardSizeY; y++)
        {
            for (int x = 0; x < gbsei.boardSizeX; x++)
            {
                //Change all the tiles on the boards type to NONE be sending the set tile type message
                SetTileTypeEvent sttei = new SetTileTypeEvent();
                sttei.type = TileType.NONE;
                sttei.pos = new Vector2(x, y);
                sttei.FireEvent();
            }
        }
    }
}

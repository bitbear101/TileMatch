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
        GetBoardEvent gbei = new GetBoardEvent();
        gbei.FireEvent();
        //Loop through the entire board        
        for (int y = 0; y < gbei.board.GetLength(1); y++)
        {
            for (int x = 0; x < gbei.board.GetLength(0); x++)
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

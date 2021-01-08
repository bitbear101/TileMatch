using Godot;
using System;
using EventCallback;
public class SwapTiles : Node
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Register the swap tile event listener
        SwapTilesEvent.RegisterListener(OnSwapTileEvent);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }

    private void OnSwapTileEvent(SwapTilesEvent stei)
    {
        //Get the direction of drag
        Vector2 dir = stei.dragEndPos - stei.dragStartPos;
        //Check if the x movement is bigger than the y movement
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            //If the x value is positive
            if (dir.x > 0)
            {
            }
            //If the x value was negative
            else
            {
            }
        }
        //If the drag was more on the y axis
        else
        {
            //If the y value is positive
            if (dir.y > 0)
            {
            }
            else
            {
            }
        }
    }
}

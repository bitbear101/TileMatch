using Godot;
using System;
using EventCallback;
public class TileInputHandler : Node
{
    //If the drag is has finnished
    bool dragging = false;
    public void OnInteractionAreaInputEvent(Viewport viewport, Godot.InputEvent @event, int shape_idx)
    {
        //If the input click or tap is dragged in the device
        // if (@event is InputEventScreenDrag screenDrag)
        // {
        //     //Convert the movement vector to a positive number to check if thier is movememnt
        //     Vector2 moveCheck = new Vector2(Mathf.Abs(screenDrag.Relative.x), Mathf.Abs(screenDrag.Relative.y));
        //     //If the drag movement is greater than one we move the camera so we don't make micro udjustments every time we acidentally touch the screen
        //     if (moveCheck > Vector2.One)
        //     {
        //         //If dragging is true return out of the method
        //         if (dragging) return;
        //         //Set the dragging to true
        //         dragging = true;
        //         SwapTilesEvent stei = new SwapTilesEvent();
        //         //Send the swipe direction
        //         //If the drag was more on the x axis
        //         if (Mathf.Abs(screenDrag.Relative.x) > Mathf.Abs(screenDrag.Relative.y))
        //         {
        //             //If the x value is positive
        //             if (screenDrag.Relative.x > 0)
        //             {
        //                 //Send the swipe direction
        //                 stei.swipeDirection = Vector2.Right;
        //             }
        //             //If the x value was negative
        //             else
        //             {
        //                 //Send the swipe direction
        //                 stei.swipeDirection = Vector2.Left;
        //             }
        //         }
        //         //If the drag was more on the y axis
        //         else
        //         {
        //             //If the y value is positive
        //             if (screenDrag.Relative.y > 0)
        //             {
        //                 //Send the swipe direction
        //                 stei.swipeDirection = Vector2.Up;
        //             }
        //             else
        //             {
        //                 //Send the swipe direction
        //                 stei.swipeDirection = Vector2.Down;
        //             }
        //         }
        //         //Send the instance ID of the tile
        //         stei.tileID = GetParent().GetInstanceId();
        //         stei.FireEvent();
        //     }
        //     else
        //     {
        //         GD.Print("Dragging done");
        //         //If the relative movement of the drag is les than vector2.one we can assume the drag is finnished
        //         dragging = false;
        //     }
        // }
        //If the device picks up on a click or tap on the screen
        if (@event is InputEventScreenTouch screenTouch)
        {
            //If the pressed values is true this code is run
            if (screenTouch.Pressed)
            {
                GD.Print("Dragging started");
                //Temp destroy tile 
                // TileDestroyedEvent tdei = new TileDestroyedEvent();
                // tdei.tileID = GetParent().GetInstanceId();
                // tdei.FireEvent();
                //Code to run on a click or tap event
                GD.Print("Tile - OnInteractionAreaInputEvent: Screen was touched or clicked");
            }
            else
            {
                GD.Print("Dragging done");
            }
        }
    }
}

using Godot;
using System;
using EventCallback;
public class TileInputHandler : Node
{
    public void OnInteractionAreaInputEvent(Viewport viewport, Godot.InputEvent @event, int shape_idx)
    {
        //If the input click or tap is dragged in the device
        if (@event is InputEventScreenDrag screenDrag)
        {
            //Convert the movement vector to a positive number to check if thier is movememnt
            Vector2 moveCheck = new Vector2(Mathf.Abs(screenDrag.Relative.x), Mathf.Abs(screenDrag.Relative.y));
            //If the drag movement is greater than one we move the camera so we don't make micro udjustments every time we acidentally touch the screen
            if (moveCheck > Vector2.One)
            {
                //What ot do if the input is dragged
                GD.Print("Tile - OnInteractionAreaInputEvent: Screen was dragged");
            }
        }
        //If the device picks up on a click or tap on the screen
        if (@event is InputEventScreenTouch screenTouch)
        {
            //If the pressed values is true this code is run
            if (screenTouch.Pressed)
            {
                //Temp destroy tile 
                //TileDestroyedEvent tdei = new TileDestroyedEvent();
                //tdei.tileID = GetParent().GetInstanceId();
                //tdei.FireEvent();
                //Code to run on a click or tap event
                GD.Print("Tile - OnInteractionAreaInputEvent: Screen was touched or clicked");
            }
        }
    }
}

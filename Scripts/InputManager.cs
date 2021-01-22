using Godot;
using System;
using EventCallback;

public class InputManager : Node
{
    //The ray used to detect collisions with objects
    RayCast2D touchRay;
    //If a tile was touched, used for to correct the message sending 
    bool tileTouched = false;
    //The touch positions
    Vector2 touchStart;
    //Object touched
    ulong nodeID;
    public override void _Ready()
    {
        //Grab a reference to the ray node in teh scene
        touchRay = GetNode<RayCast2D>("TouchRay");
        //Set the ray so it detects collisions with areas
        touchRay.CollideWithAreas = true;
    }
    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        //If there was a touch screen event
        if (@event is InputEventScreenTouch screenTouch)
        {
            //Refference to the event callback system for the input 
            InputManagerEvent imei = new InputManagerEvent();
            //The swap tile event
            SwapTilesEvent stei = new SwapTilesEvent();
            //If the screenTouch was pressed
            if (screenTouch.Pressed)
            {
                //Enadble the touchRay
                touchRay.Enabled = true;
                //Set its current position to the taps position
                touchRay.Position = screenTouch.Position;
                //Forces the raycast to update and detect the collision with the building object
                touchRay.ForceRaycastUpdate();
                //Check if there is a collision from the touch array
                if (touchRay.IsColliding())
                {
                    //Get the node that the ray collided with
                    Node2D hitNode = touchRay.GetCollider() as Node2D;
                    //check if the node has a parent, this is done because the area collider for detecting pressed is a child of the main node 
                    if (hitNode.GetParent() != null)
                    {
                        //Check if the node belongs to the building group to know what meny to call up
                        if (hitNode.GetParent().IsInGroup("Tile"))
                        {
                            //Get the instance id of the tile
                            nodeID = hitNode.GetParent().GetInstanceId();
                            //Set the start position of the drag
                            touchStart = screenTouch.Position;
                            //If a node was touched we set this to true
                            tileTouched = true;
                        }
                    }
                }
            }
            else
            {
                //If the tile has been touched and the touch is released we can send the message for touch event
                if (tileTouched)
                {
                    //Set the start position of the drag
                    stei.dragStartPos = touchStart;
                    //Set the start position of the drag
                    stei.dragEndPos = screenTouch.Position;
                    //Fire the input manager event
                    stei.FireEvent();
                    //After sending the message to the event handler we reset the tiletTouched bool
                    tileTouched = false;
                }
            }
        }
    }
}

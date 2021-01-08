using Godot;
using System;
using EventCallback;

public class InputManager : Node
{
    //The ray used to detect collisions with objects
    RayCast2D touchRay;
    //Detect when the screen touch is released to bring up the menu
    bool screenTouched = false, screenReleased = false, dragging = false;

    public override void _Ready()
    {
        //Grab a reference to the ray node in teh scene
        touchRay = GetNode<RayCast2D>("TouchRay");
        //Set the ray so it detects collisions with areas
        touchRay.CollideWithAreas = true;
    }
    public override void _UnhandledInput(Godot.InputEvent @event)
    {
        if (@event is InputEventScreenTouch screenTouch)
        {
            //Refference to the event callback system for the input 
            InputManagerEvent imei = new InputManagerEvent();

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
                    GD.Print("InputManager - _UnhandledEvent: touchRay.IsColliding() = " + touchRay.IsColliding());
                    //Get the node that the ray collided with
                    Node2D hitNode = touchRay.GetCollider() as Node2D;
                    //check if the node has a parent, this is done because the area collider for detecting pressed is a child of the main obbject 
                    if (hitNode.GetParent() != null)
                    {
                        //Check if the node belongs to the building group to know what meny to call up
                        if (hitNode.GetParent().IsInGroup("Building"))
                        {
                        }
                    }
                    //If the node that was hit by the ray does not have a parrent we close the build menu if not yet clossed
                    else
                    {
                    }
                }
                //If the screen is touched and there was no collision we close the build menu just in case it was open
                else
                {
                }
            }
            //Fire the input manager event
            imei.FireEvent();
        }

    }
}

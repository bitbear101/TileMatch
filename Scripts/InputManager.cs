using Godot;
using System;
using EventCallback;

public class InputManager : Node
{
    InputHandleEvent ihei;
    BuildMenuEvent bmei;
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
        //Refference to the event callback system for the input 
        ihei = new InputHandleEvent();
        bmei = new BuildMenuEvent();

        if (@event is InputEventScreenDrag screenDrag)
        {
            //Convert the movement vector to a positive number to check if thier is movememnt
            Vector2 moveCheck = new Vector2(Mathf.Abs(screenDrag.Relative.x), Mathf.Abs(screenDrag.Relative.y));
            //If the drag movement is greater than one we move the camera so we don't make micro udjustments every time we acidentally touch the screen
            if (moveCheck > Vector2.One)
            {
                CameraManagerEvent cmei = new CameraManagerEvent();
                cmei.draggingCamera = true;
                cmei.dragMovememnt = (screenDrag.Relative * -2);
                cmei.FireEvent();
            }
            else
            {
                CameraManagerEvent cmei = new CameraManagerEvent();
                cmei.draggingCamera = false;
                cmei.dragMovememnt = Vector2.Zero;
                cmei.FireEvent();
            }
        }

        if (@event is InputEventScreenTouch screenTouch)
        {
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
                    //check if the node has a parent, this is done because the area collider for detecting pressed is a child of the main obbject 
                    if (hitNode.GetParent() != null)
                    {
                        //Check if the node belongs to the building group to know what meny to call up
                        if (hitNode.GetParent().IsInGroup("Building"))
                        {
                            bmei.building = (Node2D)hitNode.GetParent();
                            bmei.visible = true;
                            bmei.FireEvent();
                        }
                    }
                    //If the node that was hit by the ray does not have a parrent we close the build menu if not yet clossed
                    else
                    {
                        bmei.visible = false;
                        bmei.FireEvent();
                    }
                }
                //If the screen is touched and there was no collision we close the build menu just in case it was open
                else
                {
                    bmei.visible = false;
                    bmei.FireEvent();
                }
            }
        }
        ihei.FireEvent();
    }
}

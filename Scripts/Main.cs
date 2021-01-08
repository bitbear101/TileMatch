using Godot;
using System;

public class Main : Node2D
{
    //The board scene for the game
    PackedScene boardScene = new PackedScene();
    //The input manager for the game
    PackedScene inputManagerScene = new PackedScene();
    //The node for the board after instanciation
    Node2D board;
    //The node fot het input manager
    Node inputManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set the boardScene to the object stored in the heirarchy
        boardScene = ResourceLoader.Load("res://Scenes/BoardManager.tscn") as PackedScene;
        //Setting the board node to the instanced scene
        board = (Node2D)boardScene.Instance();
        //Setting the board nodes new position
        board.Position = new Vector2(500, 300);
        //Adding the board node to the scene as a child of the initiator
        AddChild(board);

        //Set the InputManagerScene to the object in the heirarchy
        inputManagerScene = ResourceLoader.Load("res://Scenes/InputManager.tscn") as PackedScene;
        //Set the node to the instanced scene for the input manager
        inputManager = inputManagerScene.Instance();
        //Add the input manager node as a child of ht einstantiating object into the game scene
        AddChild(inputManager);

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

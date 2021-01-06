using Godot;
using System;

public class Main : Node2D
{
    //The board fot hte tiles
    PackedScene boardScene = new PackedScene();
    Node2D board;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        boardScene = ResourceLoader.Load("res://Scenes/BoardManager.tscn") as PackedScene;
        board = (Node2D)boardScene.Instance();
        board.Position = new Vector2(500, 300);
        AddChild(board);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}

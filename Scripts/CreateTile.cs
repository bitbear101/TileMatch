using Godot;
using System;
using EventCallback;
public class CreateTile : Node2D
{
    //The scene for the tile object
    PackedScene tileScene;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Set up the tile create listener
        CreateTileEvent.RegisterListener(OnCreateTileEvent);
        //Get the tile scene
        tileScene = ResourceLoader.Load("res://Scenes/Tile.tscn") as PackedScene;
    }
    private void OnCreateTileEvent(CreateTileEvent ctei)
    {
        /*
        if (ctei.emptyTopRowSlotPos.Count > 0)
        {
            //Interate through the top row of the board and spawn new tiles in
            for (int j = 0; j < ctei.emptyTopRowSlotPos.Count; j++)
            {
                //Instantiate the tilscene and set the boards node to the scene instanced
                ctei.board[(int)ctei.emptyTopRowSlotPos[j].x, (int)ctei.emptyTopRowSlotPos[j].y] = ((Node2D)tileScene.Instance());
                //Set the position of the tile on the board representation in the viewport
                ctei.board[(int)ctei.emptyTopRowSlotPos[j].x, (int)ctei.emptyTopRowSlotPos[j].y].Position = new Vector2((int)ctei.emptyTopRowSlotPos[j].x * 32, (int)ctei.emptyTopRowSlotPos[j].y * 32);
                //Add the tile as a child to the board scene
                AddChild(ctei.board[(int)ctei.emptyTopRowSlotPos[j].x, (int)ctei.emptyTopRowSlotPos[j].y]);
            }

            //Once the tile is in position the state is changed back to the check for voids state
            BoardStateChangeEvent bscei = new BoardStateChangeEvent();
            bscei.newState = BoardState.CHECK_VOIDS;
            bscei.FireEvent();
            GD.Print("BoardManager - MoveTile: Done");
        }
        */
    }
}

using Godot;
using System;
using EventCallback;

    public class UnitDeathEvent : Event<UnitDeathEvent>
    {
        public Node2D UnitNode;
        /*

        Info about cause of death, our killer, etc...

        Could be a struct, readonly, etc...

        */
    }

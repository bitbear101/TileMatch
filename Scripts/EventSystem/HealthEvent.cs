using Godot;
using System;

namespace EventCallback
{
    public class HealthEvent : Event<HealthEvent>
    {
        //The health of the object
        public int health;
        //The object that is loosing health
        public Node2D target;
    }

}
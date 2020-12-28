using Godot;
using System;

namespace EventCallback
{
    public class MissilePickupEvent : Event<MissilePickupEvent>
    {
        public bool missileUpgrade = false;
    }
}
using Godot;
using System;

namespace EventCallback
{
    public class MainEvent : Event<MainEvent>
    {
        public bool startBtnPressed, menuBtnPressed, exitBtnPressed;
    }

}
using Godot;
using System;

namespace EventCallback
{
    public class UIEvent : Event<UIEvent>
    {

        //Bools used for the ui minipulation
        public bool menuActive;
        public bool uiActive;
        public bool winActive;
        public bool loseActive;
        public bool creditsActive;
        public bool countdownActive;
        //The health to display for the player
        public int health;
        //The amount of kills the player has racked up
        public int kills;
        //The amount of time the before the new wave starts
        public int WaveTimeCountdown;
        public int level;
    }
}

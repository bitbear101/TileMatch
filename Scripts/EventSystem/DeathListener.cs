using System.Collections;
using System.Collections.Generic;
using Godot;

namespace EventCallback
{
    public class DeathListener
    {

        // Use this for initialization
        void Start()
        {
            UnitDeathEvent.RegisterListener(OnUnitDied);
        }

        void OnDestroy()
        {
            UnitDeathEvent.UnregisterListener(OnUnitDied);
        }
        void OnUnitDied(UnitDeathEvent unitDeath)
        {
            GD.Print("Alerted about unit death: " + unitDeath.UnitNode.Name);
        }
    }
}
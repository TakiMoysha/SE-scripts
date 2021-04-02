using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;

public sealed class LCDPlaceInContainer : MyGridProgram
{
    int CurrentTick;
    int Clock = 15;
    int fullness;

    public void Main(string args)
    {
        CurrentTick++;

        float capacity = 0f;
        float volume = 0f;
        float weight = 0f;

        IMyTimerBlock timer = GridTerminalSystem.GetBlockWithName("Timer") as IMyTimerBlock;
        IMyTextPanel LCDLeft = GridTerminalSystem.GetBlockWithName("LCD Left") as IMyTextPanel;
        IMyTextPanel LCDRight = GridTerminalSystem.GetBlockWithName("LCD Right") as IMyTextPanel;
        List<IMyTerminalBlock> cargoContainers = new List<IMyTerminalBlock>();
        List<IMyTerminalBlock> shipDrills = new List<IMyTerminalBlock>();

        GridTerminalSystem.SearchBlocksOfName("Cargo", cargoContainers);
        GridTerminalSystem.SearchBlocksOfName("Drill", shipDrills);
        timer.ApplyAction("TriggerNow");

        if(CurrentTick%Clock != 0)
        {
            return;
        }

        if(LCDLeft != null & LCDRight != null)
        {
        foreach (IMyShipDrill cont in shipDrills)
        {
            capacity += (float)cont.GetInventory(0).MaxVolume;
            volume += (float)cont.GetInventory(0).CurrentVolume;
            weight += (float)cont.GetInventory(0).CurrentMass;
        }

        foreach (IMyCargoContainer cont in cargoContainers)
        {
            capacity += (float)cont.GetInventory(0).MaxVolume;
            volume += (float)cont.GetInventory(0).CurrentVolume;
            weight += (float)cont.GetInventory(0).CurrentMass;
        }

        Echo($"Capacity: {capacity}");
        Echo($"Used:     {volume}");
        Echo($"Percentage: {volume / capacity}");

        LCDLeft.WriteText("Емкость: " + capacity + "\nЗанято: " + volume + "\n");
        LCDRight.WriteText(cargoContainers.Count.ToString());
        }

    }
}

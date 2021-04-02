#region Prelude
using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using VRage.Collections;
using Sandbox.ModAPI.Ingame;
using VRage.Game.Components;
using VRage.Game.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using Sandbox.Game.EntityComponents;
using SpaceEngineers.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
public sealed class Program : MyGridProgram
{
    string botName = "Earth Spy";
    string ownerName = "TakiMoysha";

    public Program() {

    }

    public void InitSystem() {
        List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
        List<IMyBlockGroup> gyros = new List<IMyBlockGroup>();
        List<IMyGyro> group = new List<IMyGyro>();
    }

    public void Main(string argument, UpdateType updateSource) {
        GridTerminalSystem.GetBlockOfType(blocks, g => g.IsWorking);
        // GridTerminalSystem.GetBlockGroupWithName

    }

    public void Save() {

    }

}

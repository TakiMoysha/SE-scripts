using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System;
using VRage.Collections;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game;
using VRage;
using VRageMath;

namespace Script
{
    partial class Program : MyGridProgram
    {
        public Program()
        {

        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {

            IMyTextPanel Screen = GridTerminalSystem.GetBlockWithName("Top LCD") as IMyTextPanel;
			IMyTextPanel ScreenII = GridTerminalSystem.GetBlockWithName("LCD") as IMyTextPanel;

            GridTerminalSystem.GetBlockGroupWithName("Engine");
            if (Screen != null) {
                Screen.WriteText("Welcome to this WORLD\t", true);
                Screen.WriteText("!!!!\n", true);
            }

        }
    }
}


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
// Этот скрипт показывает св-ва и методы блока
public sealed class AbouteBlock : MyGridProgram
{
    //Проверять при каждой компиляции:
    string LCD = "LCDPBBig"; // Куда выводить
    // В аргументе указывать имя блока о котором нужно вывести информацию
    //--------------------------------
    IMyTerminalBlock jd;
    IMyTextPanel LCDPB;
    
    AbouteBlock(string args)
    {
        jd = GridTerminalSystem.GetBlockWithName(args) as IMyTerminalBlock;
        LCDPB = GridTerminalSystem.GetBlockWithName(LCD) as IMyTextPanel;
    }

    void Main()
    {
        var actions = new List<ITerminalAction>();
        var properties = new List<ITerminalProperty>();

        jd.GetProperties(properties);
        string log = "";
        log += "---Properties";
        foreach (var i in properties){
            log += "\n" + i.Id;
        }

        jd.GetActions(actions);
        log += "\n\n---Actions";
        foreach (var i in actions){
            log += "\n" + i.Id;
        }

        LCDPB.WriteText(log);
    }
}

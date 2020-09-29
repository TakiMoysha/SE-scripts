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
// Отображает информацию о контейнерах машины
public sealed class Cargo : MyGridProgram
{
    const String CockpitName = "Cockpit";
    IMyTextSurface LCDCargoInfo;
    List<IMyCargoContainer> containers = new List<IMyCargoContainer>();
    List<IMyFunctionalBlock> shiptools = new List<IMyFunctionalBlock>();
    IMyTextSurface panel;
    RectangleF viewport;

    Cargo()
    {
        Runtime.UpdateFrequency = UpdateFrequency.Update10;
        
        containers = FindShipBlocks<IMyCargoContainer>();     
        LCDCargoInfo = GridTerminalSystem.GetBlockWithName("LCDCargoInfo") as IMyTextPanel;  

        IMyTextSurfaceProvider cockpit = (IMyTextSurfaceProvider)GridTerminalSystem.GetBlockWithName(CockpitName);

        panel = cockpit.GetSurface(0);
        viewport = new RectangleF(
        (panel.TextureSize - panel.SurfaceSize) / 2f,
        panel.SurfaceSize
        );
        panel.ContentType = ContentType.SCRIPT;
        panel.Script = "";
        panel.BackgroundColor = Color.Blue; 
    }

    void Main()
    {
        IMyTextSurfaceProvider cockpit = (IMyTextSurfaceProvider)GridTerminalSystem.GetBlockWithName(CockpitName);
        // Echo("test" + LCDCargoInfo.GetType());
        // LCDCargoInfo.WriteText(""+cockpit.GetType());


        float capacity = 0f;
        float volume = 0f;
        float weight = 0f;

        foreach (IMyCargoContainer cont in containers) {
            volume += (float)cont.GetInventory(0).CurrentVolume;
            capacity += (float)cont.GetInventory(0).MaxVolume;
            weight += (float)cont.GetInventory(0).CurrentMass;
        }
        Echo($"Capacity: {capacity}");
        Echo($"Used:     {volume}");
        Echo($"Percentage: {volume / capacity}");

    }

    void DrawPercentage(float volume, float capacity, float weight) {
        float percentage = (float)Math.Round(volume / capacity, 4);

        MySpriteDrawFrame frame = panel.DrawFrame();

        frame.Add(new MySprite() {
            Type = SpriteType.TEXTURE,
            Data = "Grid",
            Position = viewport.Center,
            Size = viewport.Size * 4,
            Color = Color.White.Alpha(0.25f),
            Alignment = TextAlignment.CENTER
        });

        frame.Add(new MySprite() {
            Type = SpriteType.TEXT,
            Data = $"{(percentage * 100)}%",
            Position = viewport.Center - new Vector2(0, 20), // Center but 20 pixels up, (according to MDK-SE that's 1 line) https://github.com/malware-dev/MDK-SE/wiki/Text-Panels-and-Drawing-Sprites#text-sprites
            RotationOrScale = 0.8f,
            Color = Color.White,
            Alignment = TextAlignment.CENTER,
            FontId = "DEBUG"
        });

        frame.Add(new MySprite() {
            Type = SpriteType.TEXT,
            Data = $"{(int)(weight + 0.5f)} kg",
            Position = viewport.Center + new Vector2(0, 20),
            RotationOrScale = 1.2f,
            Color = Color.White,
            Alignment = TextAlignment.CENTER,
            FontId = "DEBUG"
        });

        //Background of bar
        frame.Add(new MySprite() {
            Type = SpriteType.TEXTURE,
            Data = "White screen",
            Position = viewport.Center + new Vector2(0, 10),
            Size = new Vector2(viewport.Width - 80, 10),
            Color = Color.White.Alpha(0.05f),
            Alignment = TextAlignment.CENTER
        });

        // Filled part of bar
        frame.Add(new MySprite() {
            Type = SpriteType.TEXTURE,
            Data = "White screen",
            Position = new Vector2(40, viewport.Center.Y + 10),
            Size = new Vector2((viewport.Width - 80) * percentage, 10),
            Color = Color.White.Alpha(0.80f),
            Alignment = TextAlignment.LEFT
        });


        frame.Dispose();
    }

    List<BlockType> FindShipBlocks<BlockType>() where BlockType : class, IMyTerminalBlock {
    List<BlockType> blocks = new List<BlockType>();
    GridTerminalSystem.GetBlocksOfType(blocks, b => b.IsSameConstructAs(Me));
    return blocks;
    }
}

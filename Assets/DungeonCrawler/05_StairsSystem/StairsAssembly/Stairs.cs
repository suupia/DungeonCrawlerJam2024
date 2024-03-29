#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Stairs: IGridEntity
    {
        readonly DungeonSwitcher _dungeonSwitcher;

        public Stairs(DungeonSwitcher dungeonSwitcher)
        {
            _dungeonSwitcher = dungeonSwitcher;
        }

        public void GotOn()
        {
            StairsDown();
        }
        
        void StairsDown()
        {
            Debug.Log($"Stairs.StairsDown()");
            _dungeonSwitcher.SwitchNextDungeon();
        }
    }
}
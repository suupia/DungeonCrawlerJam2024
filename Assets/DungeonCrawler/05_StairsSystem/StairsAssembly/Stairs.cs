#nullable enable
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Stairs: IGridEntity
    {
        readonly MapSwitcher _mapSwitcher;

        public Stairs(MapSwitcher mapSwitcher)
        {
            _mapSwitcher = mapSwitcher;
        }
        
        public void StairsDown()
        {
            Debug.Log($"Stairs.StairsDown()");
            _mapSwitcher.SwitchNextDungeon();
        }
    }
}
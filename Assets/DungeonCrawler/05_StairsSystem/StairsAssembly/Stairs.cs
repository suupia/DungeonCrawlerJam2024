#nullable enable
using DungeonCrawler.MapAssembly.Classes;

namespace DungeonCrawler
{
    public class Stairs
    {
        readonly MapSwitcher _mapSwitcher;

        public Stairs(MapSwitcher mapSwitcher)
        {
            _mapSwitcher = mapSwitcher;
        }
        
        public void StairsDown()
        {
            _mapSwitcher.SwitchNextDungeon();
        }
    }
}
#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;

namespace DungeonCrawler.MapAssembly.Classes
{
    public class DungeonGridMap
    {
        readonly EntityGridMap _map;
        readonly IEntity _playerSpawnPosition;
        
        public DungeonGridMap(EntityGridMap map)
        {
            _map = map;
        }
        
    }
}
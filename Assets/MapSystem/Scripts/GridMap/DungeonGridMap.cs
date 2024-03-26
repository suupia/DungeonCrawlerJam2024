#nullable enable
using DungeonCrawler.MapSystem.Interfaces;

namespace DungeonCrawler.MapSystem.Scripts
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
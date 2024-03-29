#nullable enable
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap
{
    public class GridEntityFactory
    {
        readonly DungeonSwitcher _dungeonSwitcher;
        [Inject]
        public GridEntityFactory(DungeonSwitcher dungeonSwitcher)
        {
            _dungeonSwitcher = dungeonSwitcher;
        }
        
        public IGridEntity CreateEntity<TEntity> () where  TEntity : IGridEntity
        {
            if (typeof(TEntity) == typeof(Stairs))
            {
                return new Stairs(_dungeonSwitcher);
            }
            if (typeof(TEntity) == typeof(Enemy))
            {
                return new Enemy();
            }
            return new DefaultEntity();
        }
    }
}
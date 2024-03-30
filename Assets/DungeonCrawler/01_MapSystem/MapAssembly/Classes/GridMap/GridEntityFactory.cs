#nullable enable
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap
{
    public class GridEntityFactory
    {
        BattleGameConnector _battleGameConnector;
        HangerSystem _hangerSystem;
        TorchSystem _torchSystem;
        
        [Inject]
        public GridEntityFactory(
            BattleGameConnector battleGameConnector,
            HangerSystem hangerSystem,
            TorchSystem torchSystem
            )
        {
            _battleGameConnector = battleGameConnector;
            _hangerSystem = hangerSystem;
            _torchSystem = torchSystem;
        }
        public IGridEntity CreateEntity<TEntity> (DungeonSwitcher dungeonSwitcher) where  TEntity : IGridEntity
        {
            return typeof(TEntity) switch
            {
                _ when typeof(TEntity) == typeof(Player) => new Player(),
                _ when typeof(TEntity) == typeof(Stairs) => new Stairs(dungeonSwitcher),
                _ when typeof(TEntity) == typeof(Enemy) => new Enemy(_battleGameConnector,dungeonSwitcher),
                _ when typeof(TEntity) == typeof(Torch) => new Torch(_torchSystem, dungeonSwitcher),
                _ when typeof(TEntity) == typeof(Food) => new Food(_hangerSystem, dungeonSwitcher),
                _ when typeof(TEntity) == typeof(DefaultEntity) => new DefaultEntity(),
                _ => new DefaultEntity(),
            };
        }
    }
}
﻿#nullable enable
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapAssembly.Interfaces;
using VContainer;

namespace DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap
{
    public class GridEntityFactory
    {
        public IGridEntity CreateEntity<TEntity> (DungeonSwitcher dungeonSwitcher) where  TEntity : IGridEntity
        {
            if (typeof(TEntity) == typeof(Stairs))
            {
                return new Stairs(dungeonSwitcher);
            }
            if (typeof(TEntity) == typeof(Enemy))
            {
                return new Enemy();
            }
            return new DefaultEntity();
        }
    }
}
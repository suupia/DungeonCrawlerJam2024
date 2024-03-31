#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes;
using DungeonCrawler._01_MapSystem.MapAssembly.Classes.GridMap;
using DungeonCrawler._04_EnemySystem.EnemyAssembly;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Classes.Entity;
using DungeonCrawler.MapMonoAssembly;
using DungeonCrawler.PlayerAssembly.Interfaces;
using DungeonCrawler.PlayerMonoAssembly;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DungeonCrawler
{
    public class GameSystemScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // GameState
            builder.Register<GameStateSwitcher>(Lifetime.Scoped);

            builder.Register<SquareGridCoordinate>(Lifetime.Scoped).As<IGridCoordinate>().WithParameter("width",50).WithParameter("height",50);
            builder.Register<DivideAreaExecutor>(Lifetime.Scoped);
            builder.Register<DungeonBuilder>(Lifetime.Scoped);

            // IGridEntity
            builder.Register<GridEntityFactory>(Lifetime.Scoped);
            
            // GridMap
            builder.Register<DefaultDungeonGridMap>(Lifetime.Scoped);
            builder.Register<GridTilePlacer>(Lifetime.Scoped);
            builder.Register<DungeonSwitcher>(Lifetime.Scoped);
            
            // Battle
            builder.Register<BattleSimulator>(Lifetime.Scoped);
            builder.Register<BattleGameConnector>(Lifetime.Scoped);
            
            // Player
            builder.Register<PlayerStats>(Lifetime.Scoped);
            
            // Enemy
            
            // Hanger
            builder.Register<HangerMeter>(Lifetime.Scoped).WithParameter("maxValue",50);
            builder.Register<HangerSystem>(Lifetime.Scoped);

            // FlamePoint
            builder.Register<FlamePoint>(Lifetime.Scoped);

            // Torch
            builder.Register<TorchInventory>(Lifetime.Scoped).WithParameter("initValue", 0);
            builder.Register<TorchSystem>(Lifetime.Scoped);


            // Mono
            builder.RegisterComponentInHierarchy<MapBuilderMono>();
            builder.RegisterComponentInHierarchy<PlayerSpawnerMono>();
            builder.RegisterComponentInHierarchy<GameSystemInitializer>();
            builder.RegisterComponentInHierarchy<EnemySpawnerMono>();
            builder.RegisterComponentInHierarchy<StairsSpawnerMono>();
            builder.RegisterComponentInHierarchy<StairsSpawnerMono>();
            builder.RegisterComponentInHierarchy<TorchSpawnerMono>();
            builder.RegisterComponentInHierarchy<MiniMapManagerMono>();
            builder.RegisterComponentInHierarchy<FoodSpawnerMono>();
            
            // UI Mono
            builder.RegisterComponentInHierarchy<UIDomainConnectorMono>();
            builder.RegisterComponentInHierarchy<FloorUIMono>();
            builder.RegisterComponentInHierarchy<BattleUIInputMono>();
            builder.RegisterComponentInHierarchy<BattleUIOutputMono>();
            builder.RegisterComponentInHierarchy<HangerUIMono>();
            builder.RegisterComponentInHierarchy<TorchUIMono>();
            builder.RegisterComponentInHierarchy<StatsUIMono>();
            
            builder.RegisterComponentInHierarchy<TitleInputMono>();
        }
    }
}

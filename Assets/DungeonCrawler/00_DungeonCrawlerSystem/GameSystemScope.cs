#nullable enable
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.MapAssembly.Classes;
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
            // Player
            // builder.Register<CarryPlayerFactory>(Lifetime.Scoped).As<ICarryPlayerFactory>();
            // builder.Register<CarryPlayerControllerNetBuilder>(Lifetime.Scoped).As<IPlayerControllerNetBuilder>();
            // builder.Register<NetworkPlayerSpawner>(Lifetime.Scoped);
            // builder.Register<CarryPlayerContainer>(Lifetime.Scoped);

            builder.Register<SquareGridCoordinate>(Lifetime.Scoped).As<IGridCoordinate>().WithParameter("width",50).WithParameter("height",50);
            builder.Register<DivideAreaExecutor>(Lifetime.Scoped);
            builder.Register<DungeonBuilder>(Lifetime.Scoped);
            
            builder.Register<MapSwitcher>(Lifetime.Scoped);
            
            builder.RegisterComponentInHierarchy<MapBuilderMono>().As<IMapBuilderMono>();
            builder.RegisterComponentInHierarchy<PlayerSpawnerMono>().As<IPlayerSpawnerMono>();
            builder.RegisterComponentInHierarchy<GameSystemInitializer>();
            builder.RegisterComponentInHierarchy<EnemySpawnerMono>();
            builder.RegisterComponentInHierarchy<StairsSpawnerMono>();
            builder.RegisterComponentInHierarchy<PlayerController>();
            builder.RegisterComponentInHierarchy<StairsSpawnerMono>();
            builder.RegisterComponentInHierarchy<TorchSpawnerMono>();
        }
    }
}

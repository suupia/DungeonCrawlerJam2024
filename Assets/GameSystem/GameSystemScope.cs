using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.MapSystem.Interfaces;
using DungeonCrawler.Runtime.Player;
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

            builder.RegisterComponentInHierarchy<PlayerSpawnerMonoSystem>();
            builder.RegisterComponentInHierarchy<MapBuilderMonoSystem>();
            builder.RegisterComponentInHierarchy<GameSystemInitializer>();

        }
    }
}

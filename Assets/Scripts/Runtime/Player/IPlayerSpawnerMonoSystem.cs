#nullable enable
using DungeonCrawler.Core;
using DungeonCrawler.MapSystem.Scripts;
using UnityEngine;

namespace DungeonCrawler.Runtime.Player
{
    public interface IPlayerSpawnerMonoSystem : IMonoSystem
    {
        void SpawnPlayer(int x, int y);
    }
}
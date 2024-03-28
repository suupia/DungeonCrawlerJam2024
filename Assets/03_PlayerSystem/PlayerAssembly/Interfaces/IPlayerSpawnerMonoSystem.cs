#nullable enable
using UnityEngine;

namespace DungeonCrawler.Runtime.Player
{
    public interface IPlayerSpawnerMonoSystem
    {
        void SpawnPlayer(int x, int y);
    }
}
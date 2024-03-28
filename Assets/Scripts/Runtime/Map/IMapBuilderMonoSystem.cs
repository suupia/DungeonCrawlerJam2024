#nullable enable
using DungeonCrawler.Core;
using DungeonCrawler.MapSystem.Scripts;

namespace DungeonCrawler
{
    public interface IMapBuilderMonoSystem : IMonoSystem
    {
        public void CreateDungeon();
    }
}
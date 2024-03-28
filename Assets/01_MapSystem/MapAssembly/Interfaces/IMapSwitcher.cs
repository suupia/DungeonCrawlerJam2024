#nullable enable
namespace DungeonCrawler.MapSystem.Interfaces
{
    public interface IMapSwitcher
    {
        void InitSwitchMap();
        void SwitchMap();
        void RegisterResetAction(System.Action action);
    }
}
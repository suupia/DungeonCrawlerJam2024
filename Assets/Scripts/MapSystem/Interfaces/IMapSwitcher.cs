#nullable enable
namespace DungeonCrawler.Map.Interfaces
{
    public interface IMapSwitcher
    {
        void InitSwitchMap();
        void SwitchMap();
        void RegisterResetAction(System.Action action);
    }
}
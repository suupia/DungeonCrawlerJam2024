#nullable enable
using UnityEngine;

namespace DungeonCrawler.MapSystem.Interfaces
{
    public interface IPresenterMono
    {
        public MonoBehaviour GetMonoBehaviour { get; }
        public void DestroyPresenter();
    }
}
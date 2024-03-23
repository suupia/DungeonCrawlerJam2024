#nullable enable
using UnityEngine;

namespace DungeonCrawler.Map.Interfaces
{
    public interface IPresenterMono
    {
        public MonoBehaviour GetMonoBehaviour { get; }
        public void DestroyPresenter();
    }
}
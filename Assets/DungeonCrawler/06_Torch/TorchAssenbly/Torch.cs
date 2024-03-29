#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Torch : IGridEntity
    {
        public void GotOn()
        {
            Debug.Log($"Torch.GotOn()");
        }
    }
}
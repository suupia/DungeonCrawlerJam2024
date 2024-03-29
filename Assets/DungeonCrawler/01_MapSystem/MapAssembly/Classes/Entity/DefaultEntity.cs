#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler.MapAssembly.Classes.Entity
{
    public class DefaultEntity : IGridEntity
    {
        public void GotOn()
        {
            Debug.LogWarning($"DefaultEntity.GotOn()");
        }
    }
}
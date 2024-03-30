#nullable enable

using Codice.Client.GameUI.Explorer;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Food : IGridEntity
    {
        public void GotOn()
        {
            Debug.Log("Food.GotOn()");
        }
    }
}

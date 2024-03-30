#nullable enable
using System;
using System.Diagnostics;
using UnityEngine;
using VContainer;
using Debug = UnityEngine.Debug;

namespace DungeonCrawler
{
    public class TorchSystem
    {
        TorchInventory _torchInventory;
        
        [Inject]
        public TorchSystem(TorchInventory torchInventory)
        {
            _torchInventory = torchInventory;
        }

        public void PlaceTorch(int x, int y)
        {
            Debug.Log($"place a torch at ({x}, {y})");
        }
        public void PickUpTorch()
        {
            _torchInventory.Value += 1;
        }
    }
}
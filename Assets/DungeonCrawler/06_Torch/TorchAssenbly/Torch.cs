#nullable enable
using System;
using DungeonCrawler.MapAssembly.Classes;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class Torch : IGridEntity
    {
        public event EventHandler OnPicked = (sender, e) => { };
        public Func<(int x, int y)> GridPosition = () => (0, 0);
        public int Num { get; set; } = 1;
        TorchSystem _torchSystem;

        public Torch(TorchSystem torchSystem, DungeonSwitcher dungeonSwitcher)
        {
            _torchSystem = torchSystem;
            OnPicked += (sender, e) =>
            {
                Debug.Log("Torch.OnPicked()");
                dungeonSwitcher.CurrentDungeon.Map.RemoveEntity(GridPosition().x,GridPosition().y, this);
            };
        }
        public void GotOn()
        {
            Debug.Log("Torch.GotOn()");
            _torchSystem.PickUpTorch(this);
            
            OnPicked(this, EventArgs.Empty);
        }
    }
}
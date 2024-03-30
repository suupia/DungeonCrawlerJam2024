#nullable enable
using System;
using DungeonCrawler.MapAssembly.Interfaces;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes
{
    public class Player : IGridEntity
    {
        public Func<(int x, int y)> GridPosition = () => (0, 0);
        public Func<Quaternion> CurrentRotation = () => Quaternion.identity;
        
        public void GotOn()
        {
            Debug.LogWarning("Player.GotOn()");
        }
    }
}
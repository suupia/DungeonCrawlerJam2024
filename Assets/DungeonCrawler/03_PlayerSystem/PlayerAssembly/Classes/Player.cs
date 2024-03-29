#nullable enable
using Codice.Client.BaseCommands;
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes
{
    public class Player : IGridEntity
    {
        public void GotOn()
        {
            Debug.LogWarning("Player.GotOn()");
        }
    }
}
#nullable enable
using DungeonCrawler.MapAssembly.Interfaces;
using UnityEngine;
using VContainer;

namespace DungeonCrawler._04_EnemySystem.EnemyAssembly
{
    public class Enemy : IGridEntity
    {
        GameStateSwitcher _gameState;
        
        public Enemy(GameStateSwitcher gameState)
        {
            _gameState = gameState;
        }
        
        public void GotOn()
        {
            Debug.Log($"Enemy.GotOn()");
            _gameState.EnterBattling();
            
        }
    }
}
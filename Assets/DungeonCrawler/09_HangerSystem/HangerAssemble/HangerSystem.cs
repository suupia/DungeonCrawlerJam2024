#nullable enable
using System;

namespace DungeonCrawler
{
    public class HangerSystem
    {
        
        
        public void UpdateTurn(Action action) // from PlayerController.Move()?
        {
            
        }

        void GameOver(Action action)
        {
            action();
        }
    }
}

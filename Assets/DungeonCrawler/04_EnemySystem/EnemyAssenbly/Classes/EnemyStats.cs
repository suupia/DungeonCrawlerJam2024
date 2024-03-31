#nullable enable
using System;
using DungeonCrawler.MapAssembly.Classes;
using VContainer;

namespace DungeonCrawler
{
    public class EnemyStats
    {
        public int MaxHp => CalcMaxHp();

        public int Atk => CalcAtk();

        public Func<int> CalcLevel = () => 1;

        int CalcMaxHp()
        {
            return 5 * CalcLevel();
        }

        int CalcAtk()
        {
            return CalcLevel();
        }
    }
    
    
}
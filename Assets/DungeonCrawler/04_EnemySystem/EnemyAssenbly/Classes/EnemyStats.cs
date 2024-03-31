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
        readonly Func<int> _levelFunc;

        public EnemyStats(Func<int> leveFunc)
        {
            _levelFunc = leveFunc;
        }

        int CalcMaxHp()
        {
            return 5 * _levelFunc();
        }

        int CalcAtk()
        {
            return _levelFunc();
        }
    }
    
    
}
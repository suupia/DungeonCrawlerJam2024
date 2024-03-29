using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler.EnemyAssembly.Classes
{
    public class EnemyDomain
    {
        public int _hp;
        int _attack;

        public EnemyDomain(int hp, int attack)
        {
            _hp = hp;
            _attack = attack;
        }

        public void Attack(PlayerDomain target)
        {
            target._hp -= _attack;
        }
    }
}

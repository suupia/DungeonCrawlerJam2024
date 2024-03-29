using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    public class EnemyDomain
    {
        public int Hp;
        int _attack;

        public EnemyDomain(int hp, int attack)
        {
            Hp = hp;
            _attack = attack;
        }

        public void Attack(PlayerDomain target)
        {
            target.Hp -= _attack;
        }
    }
}

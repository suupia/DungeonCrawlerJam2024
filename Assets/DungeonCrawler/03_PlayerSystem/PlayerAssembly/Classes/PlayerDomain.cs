using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.EnemyAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler.PlayerAssembly.Classes
{
    public class PlayerDomain
    {
        public int _hp;
        int _attack;


        public PlayerDomain(int hp, int attack)
        {
            _hp = hp;
            _attack = attack;
        }

        public void Attack(EnemyDomain target)
        {
            target._hp -= _attack;
        }

        public void SacredAttack(EnemyDomain target)
        {
            
        }
    }
}

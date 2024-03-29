using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerDomain
    {
        public int Hp;
        int _attack;


        public PlayerDomain(int hp, int attack)
        {
            Hp = hp;
            _attack = attack;
        }

        public void Attack(EnemyDomain target)
        {
            target.Hp -= _attack;
        }

        public void SacredAttack(EnemyDomain target)
        {
            
        }
    }
}

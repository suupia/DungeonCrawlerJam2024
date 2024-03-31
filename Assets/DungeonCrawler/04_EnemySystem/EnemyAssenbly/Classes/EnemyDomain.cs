using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    public class EnemyDomain
    {
        public int MaxHp { get; private set; }
        public int CurrentHp => MaxHp - DamagedReceived;
        public int DamagedReceived { get; private set; }
        public bool IsDead => DamagedReceived >= MaxHp;
        int _attack;

        public EnemyDomain(int maxHp, int attack)
        {
            MaxHp = maxHp;
            _attack = attack;
        }
        public void OnAttacked(int damage)
        {
            DamagedReceived += damage;
        }
        public void Attack(PlayerDomain target)
        {
            target.OnAttacked(_attack);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerDomain
    {
        public int MaxHp { get; private set; }
        public int CurrentHp => MaxHp - DamagedReceived;
        public int DamagedReceived { get; private set; }
        public bool IsDead => DamagedReceived >= MaxHp;

        public PlayerDomain(int maxHp)
        {
            MaxHp = maxHp;
        }
        
        public void OnAttacked(int damage)
        {
            DamagedReceived += damage;
        }
    }
}

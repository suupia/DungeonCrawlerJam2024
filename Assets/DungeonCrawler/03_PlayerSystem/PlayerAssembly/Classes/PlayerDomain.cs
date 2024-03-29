using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerDomain
    {
        public int Hp { get; private set; }

        public PlayerDomain(int hp)
        {
            Hp = hp;
        }
    }
}

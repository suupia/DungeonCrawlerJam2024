using System.Collections;
using System.Collections.Generic;
using DungeonCrawler.PlayerAssembly.Interfaces;
using UnityEngine;

namespace DungeonCrawler
{
    public class PlayerDomain
    {
        public int Hp;

        public PlayerDomain(int hp)
        {
            Hp = hp;
        }
    }
}

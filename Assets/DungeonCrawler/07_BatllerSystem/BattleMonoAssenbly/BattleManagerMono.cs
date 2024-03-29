using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonCrawler
{
    public class BattleManagerMono : MonoBehaviour
    {
        BattleManager _battleManager;

        void Start()
        {
            var player = new PlayerDomain(20, 2);
            var enemy = new EnemyDomain(15, 1);
            _battleManager = new BattleManager(player, enemy);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                
            }
        }
    }
}

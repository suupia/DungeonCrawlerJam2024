using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    public class BattleManagerMono : MonoBehaviour
    {
        BattleSimulator _battleSimulator;

        void Start()
        {
            var player = new PlayerDomain(20);
            var enemy = new EnemyDomain(15, 1);
            _battleSimulator = new BattleSimulator(player, enemy);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                // _battleSimulator.StartBattle();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                _battleSimulator.UpdateTurn(new NormalAttack(2));
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                _battleSimulator.UpdateTurn(new SacredAttack(4));
            }
        }
    }
}

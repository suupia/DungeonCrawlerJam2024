using System;
using System.Collections;
using System.Collections.Generic;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using UnityEngine;

namespace DungeonCrawler
{
    public class BattleManagerMono : MonoBehaviour
    {
        BattleManager _battleManager;

        void Start()
        {
            var player = new PlayerDomain(20);
            var enemy = new EnemyDomain(15, 1);
            _battleManager = new BattleManager(player, enemy);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _battleManager.StartBattle();
            }
            else if (Input.GetKeyDown(KeyCode.U))
            {
                _battleManager.UpdateTurn(new NormalAttack(2));
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                _battleManager.UpdateTurn(new SacredAttack(4));
            }
        }
    }
}

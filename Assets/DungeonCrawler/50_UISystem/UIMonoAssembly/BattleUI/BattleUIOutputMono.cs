using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class BattleUIOutputMono : MonoBehaviour
{
   [Header("Battle")]
   // Player
   [SerializeField] TextMeshProUGUI playerHpText = null!;
   [SerializeField] TextMeshProUGUI playerAttackText = null!;
   // Enemy
   [SerializeField] TextMeshProUGUI enemyHpText = null!;
   [SerializeField] TextMeshProUGUI enemyNameText = null!;
   
   [SerializeField] HpGaugeMono playerHpGauge = null!;
   [SerializeField] HpGaugeMono enemyHpGauge = null!;
   
   [Header("Result")]
   [SerializeField] TextMeshProUGUI resultText = null!;

   BattleSimulator _battleSimulator;
   [Inject]
   public void Construct(BattleSimulator battleSimulator)
   {
      _battleSimulator = battleSimulator;
      SetUp();
   }

   void SetUp()
   {
      // [Battle]
      Observable.EveryValueChanged(this, _ => _battleSimulator.Player?.CurrentHp)
         .Subscribe(_ =>
         {
            if (_battleSimulator.Player == null) return;
            playerHpText.text = $"Player HP: {_battleSimulator.Player?.CurrentHp}";
            playerHpGauge.FillRate((float)_battleSimulator.Player.CurrentHp/_battleSimulator.Player.MaxHp);
         });

      Observable.EveryValueChanged(this, _ => _battleSimulator.Enemy?.CurrentHp)
         .Subscribe(_ =>
         {
            if (_battleSimulator.Enemy == null) return;
            enemyHpText.text = $"Enemy HP: {_battleSimulator.Enemy.CurrentHp}";
            enemyHpGauge.FillRate((float)_battleSimulator.Enemy.CurrentHp/_battleSimulator.Enemy.MaxHp);
            // enemyNameText.text = $"Enemy Name: {_battleSimulator.Enemy.Name}";
         });
      
      // [Result]
      _battleSimulator.OnBattleStart += (sender, e) =>
      {
         resultText.text = "";
      };
      
      _battleSimulator.OnPlayerWin += (sender, e) =>
      {
         resultText.text = "Win!";
      };
      _battleSimulator.OnPlayerLose += (sender, e) =>
      {
         resultText.text = "Game Over!";
      };
   }
}

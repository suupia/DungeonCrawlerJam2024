using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using R3;
using TMPro;
using UnityEngine;
using VContainer;

public class BattleUIOutputMono : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI playerHpText = null!;
   [SerializeField] TextMeshProUGUI enemyHpText = null!;
   [SerializeField] TextMeshProUGUI enemyNameText = null!;

   BattleSimulator _battleSimulator;
   [Inject]
   public void Construct(BattleSimulator battleSimulator)
   {
      _battleSimulator = battleSimulator;
      SetUp();
   }

   void SetUp()
   {
      Observable.EveryValueChanged(this, _ => _battleSimulator.Player.CurrentHp)
         .Subscribe(_ =>
         {
            playerHpText.text = $"Player HP: {_battleSimulator.Player.CurrentHp}";
         });

      Observable.EveryValueChanged(this, _ => _battleSimulator.Enemy.Hp)
         .Subscribe(_ =>
         {
            enemyHpText.text = $"Enemy HP: {_battleSimulator.Enemy.Hp}";
            // enemyNameText.text = $"Enemy Name: {_battleSimulator.Enemy.Name}";
         });
   }
}

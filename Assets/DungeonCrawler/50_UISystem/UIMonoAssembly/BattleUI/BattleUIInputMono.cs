using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using TMPro;
using UnityEngine;
using VContainer;

public class BattleUIInputMono : MonoBehaviour
{
    // BattleUI
    [SerializeField] CustomButton normalAttackButton = null!;
    [SerializeField] TextMeshProUGUI normalAttackText = null!;
    [SerializeField] CustomButton sacredAttackButton = null!;
    [SerializeField] TextMeshProUGUI sacredAttackText = null!;

    // ResultUI
    [SerializeField] CustomButton returnToExploreButton = null!;
    
    GameStateSwitcher _gameStateSwitcher;
    BattleSimulator _battleSimulator;
    BattleGameConnector _battleGameConnector;
    [Inject]
    public void Construct(
        GameStateSwitcher gameStateSwitcher,
        BattleSimulator battleSimulator,
        BattleGameConnector battleGameConnector
        )
    {
        _gameStateSwitcher = gameStateSwitcher;
        _battleSimulator = battleSimulator;
        _battleGameConnector = battleGameConnector;
        SetUp();
    }
    void SetUp()
    {
        // BattleUI
        normalAttackText.text = "Normal Attack";
        sacredAttackText.text = "Sacred Attack";
        normalAttackButton.AddListener(() =>
        {
            Debug.Log("Normal Attack");
            _battleGameConnector.UpdateTurnWithNormalAttack();
        });
        sacredAttackButton.AddListener(() =>
        {
            Debug.Log("Sacred Attack");
            _battleGameConnector.UpdateTurnWithSacredAttack();
        });
        
        // ResultUI
        returnToExploreButton.AddListener(() =>
        {
                Debug.Log("Return to Explore");
            _battleGameConnector.EndBattle();
        });
    }
}

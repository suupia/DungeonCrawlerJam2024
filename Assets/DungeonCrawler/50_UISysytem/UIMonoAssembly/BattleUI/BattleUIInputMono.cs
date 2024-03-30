using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands;
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
    [Inject]
    public void Construct(
        GameStateSwitcher gameStateSwitcher,
        BattleSimulator battleSimulator
        )
    {
        _gameStateSwitcher = gameStateSwitcher;
        _battleSimulator = battleSimulator;
    }
    void Start()
    {
        // BattleUI
        normalAttackText.text = "Normal Attack";
        sacredAttackText.text = "Sacred Attack";
        normalAttackButton.AddListener(() =>
        {
            Debug.Log("Normal Attack");
            _battleSimulator.UpdateTurn(new NormalAttack(10));
        });
        sacredAttackButton.AddListener(() =>
        {
            Debug.Log("Sacred Attack");
            _battleSimulator.UpdateTurn(new SacredAttack(50));
        });
        
        // ResultUI
        returnToExploreButton.AddListener(() =>
        {
            Debug.Log("Return to Explore");
            _gameStateSwitcher.EnterExploring();
        });
    }
}

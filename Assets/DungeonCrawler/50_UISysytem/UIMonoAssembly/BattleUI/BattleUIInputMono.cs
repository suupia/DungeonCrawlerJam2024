using System.Collections;
using System.Collections.Generic;
using DungeonCrawler;
using DungeonCrawler._03_PlayerSystem.PlayerAssembly.Classes;
using TMPro;
using UnityEngine;
using VContainer;

public class BattleUIInputMono : MonoBehaviour
{
    [SerializeField] CustomButton normalAttackButton = null!;
    [SerializeField] TextMeshProUGUI normalAttackText = null!;
    [SerializeField] CustomButton sacredAttackButton = null!;
    [SerializeField] TextMeshProUGUI sacredAttackText = null!;

    BattleSimulator _battleSimulator;
    [Inject]
    public void Construct(
        BattleSimulator battleSimulator)
    {
        _battleSimulator = battleSimulator;
    }
    void Start()
    {
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
    }
}

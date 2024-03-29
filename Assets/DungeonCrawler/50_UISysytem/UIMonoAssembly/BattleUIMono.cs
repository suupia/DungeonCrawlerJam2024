using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VContainer;

public class BattleUIMono : MonoBehaviour
{
    [SerializeField] CustomButton normalAttackButton = null!;
    [SerializeField] TextMeshProUGUI normalAttackText = null!;
    [SerializeField] CustomButton sacredAttackButton = null!;
    [SerializeField] TextMeshProUGUI sacredAttackText = null!;

    [Inject]
    public void Construct()
    {
        
    }
    void Start()
    {
        normalAttackText.text = "Normal Attack";
        sacredAttackText.text = "Sacred Attack";
        normalAttackButton.AddListener(() =>
        {
            Debug.Log("Normal Attack");
        });
        sacredAttackButton.AddListener(() =>
        {
            Debug.Log("Sacred Attack");
        });
    }
}

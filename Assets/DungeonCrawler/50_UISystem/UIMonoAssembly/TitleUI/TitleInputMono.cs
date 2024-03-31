#nullable enable
using DungeonCrawler._10_UpgradeSystem.UpgradeAssembly;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Serialization;
using VContainer;
using Observable = R3.Observable; 

namespace DungeonCrawler
{
    public class TitleInputMono : MonoBehaviour
    {
        [SerializeField] CustomButton startButton = null!;
        [SerializeField] CustomButton upgradeButton = null!;
        [SerializeField] CustomButton settingsButton = null!;
        [SerializeField] TextMeshProUGUI flamePointText = null!;

        
        [Header("Upgrade")]
        [SerializeField] UpgradeUIMono upgradeUIMono;

        FlamePoint _flamePoint;
        GameStateSwitcher _gameStateSwitcher;
        
        [Inject]
        public void Init(GameStateSwitcher gameStateSwitcher, FlamePoint flamePoint)
        {
            _gameStateSwitcher = gameStateSwitcher;
            _flamePoint = flamePoint;
            
            SetUp();
        }

        void SetUp()
        {
            upgradeUIMono.Init();
            startButton.AddListener(() =>
            {
                Debug.Log($"Start Game");
                _gameStateSwitcher.EnterExploring();
            });
            upgradeButton.AddListener(() =>
            {
                Debug.Log($"Open Upgrade");
                upgradeUIMono.ShowUpgradeUI();
            });
            Observable.EveryValueChanged(this, _ => _flamePoint.FlamePointValue)
                .Subscribe(_ =>
                {
                    flamePointText.text =$"Flame point : {_flamePoint.FlamePointValue}";
                });
        }
    }
}
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
        [SerializeField] TextMeshProUGUI highScoreText = null!;

        
        [Header("Upgrade")]
        [SerializeField] UpgradeUIMono upgradeUIMono;

        FlamePoint _flamePoint;
        HighScore _highScore;
        GameStateSwitcher _gameStateSwitcher;
        
        [Inject]
        public void Init(
            GameStateSwitcher gameStateSwitcher,
            FlamePoint flamePoint,
            HighScore highScore
            )
        {
            _gameStateSwitcher = gameStateSwitcher;
            _flamePoint = flamePoint;
            _highScore = highScore;
            
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
            Observable.EveryValueChanged(this, _ => _highScore.HighScoreValue)
                .Subscribe(_ =>
                {
                    highScoreText.text =$"High Score : {_highScore.HighScoreValue}";
                });
        }
    }
}
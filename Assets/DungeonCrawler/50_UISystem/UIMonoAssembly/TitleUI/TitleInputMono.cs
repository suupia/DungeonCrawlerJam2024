#nullable enable
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;

namespace DungeonCrawler
{
    public class TitleInputMono : MonoBehaviour
    {
        [SerializeField] CustomButton startButton = null!;
        [SerializeField] CustomButton upgradeButton = null!;
        [SerializeField] CustomButton settingsButton = null!;

        
        [Header("Upgrade")]
        [SerializeField] UpgradeUIMono upgradeUIMono;
        
        
        GameStateSwitcher _gameStateSwitcher;
        
        [Inject]
        public void Init(GameStateSwitcher gameStateSwitcher)
        {
            _gameStateSwitcher = gameStateSwitcher;
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
        }
    }
}
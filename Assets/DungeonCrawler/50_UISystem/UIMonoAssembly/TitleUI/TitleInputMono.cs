#nullable enable
using UnityEngine;
using VContainer;

namespace DungeonCrawler
{
    public class TitleInputMono : MonoBehaviour
    {
        [SerializeField] CustomButton startButton = null!;
        [SerializeField] CustomButton upgradeButton = null!;
        [SerializeField] CustomButton settingsButton = null!;

        GameStateSwitcher _gameStateSwitcher;
        
        [Inject]
        public void Init(GameStateSwitcher gameStateSwitcher)
        {
            _gameStateSwitcher = gameStateSwitcher;
            SetUp();
        }

        void SetUp()
        {
            startButton.AddListener(() =>
            {
                Debug.Log($"Start Game");
                _gameStateSwitcher.EnterExploring();
            });
        }
    }
}
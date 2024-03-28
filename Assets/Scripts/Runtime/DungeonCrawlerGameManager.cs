// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using DungeonCrawler.Core;
// using DungeonCrawler.Runtime.MonoSystems.Animation;
// using UnityEngine.Serialization;
//
// namespace DungeonCrawler.Runtime
// {
//     internal class DungeonCrawlerGameManager : GameManager
//     {
//         [Header("Holders")]
//         [SerializeField] private GameObject _monoSystemParnet;
//
//         [Header("MonoSystems")]
//         [SerializeField] private AnimationMonoSystem _animationMonoSystem;
//         
//         // [SerializeField] PlayerSpawnerMonoSystem playerSpawnerMonoSystem;
//         
//         /// <summary>
//         /// Adds all events listeners
//         /// </summary>
//         private void AddListeners()
//         {
//
//         }
//
//         /// <summary>
//         /// Removes all events listeners
//         /// </summary>
//         private void RemoveListeners()
//         {
//
//         }
//
//         /// <summary>
//         /// Attaches all MonoSystems to the GameManager
//         /// </summary>
//         private void AttachMonoSystems()
//         {
//             AddMonoSystem<AnimationMonoSystem, IAnimationMonoSystem>(_animationMonoSystem);
//             
//             // AddMonoSystem<PlayerSpawnerMonoSystem , IPlayerSpawnerMonoSystem>(playerSpawnerMonoSystem);
//         }
//
//         protected override string GetApplicationName()
//         {
//             return nameof(DungeonCrawlerGameManager);
//         }
//
//         protected override void OnInitalized()
//         {
//             // Ataches all MonoSystems to the GameManager
//             AttachMonoSystems();
//
//             // Adds Event Listeners
//             AddListeners();
//
//             // Ensures all MonoSystems call Awake at the same time.
//             _monoSystemParnet.SetActive(true);
//         }
//
//         private void OnDestroy()
//         {
//             RemoveListeners();
//         }
//     }
// }

#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace DungeonCrawler
{
    public class GameStateSwitcher
    {
        enum GameStateEnum
        {
            None,
            AtTitle,
            Exploring,
            Battling,
            InSettings,
            // InInventory,
        }

        GameStateEnum _gameState = GameStateEnum.None;
        
        Dictionary<GameStateEnum, List<GameStateEnum>> _stateTransitionMap = new() // (Current State) -> (List of Possible Transition States)
        {
            { GameStateEnum.None, new() { GameStateEnum.None, GameStateEnum.AtTitle,GameStateEnum.Exploring, GameStateEnum.Battling, GameStateEnum.InSettings}}, 
            { GameStateEnum.AtTitle, new() { GameStateEnum.Exploring, GameStateEnum.InSettings}},
            { GameStateEnum.Exploring, new() { GameStateEnum.Battling, GameStateEnum.InSettings}},
            { GameStateEnum.Battling, new() { GameStateEnum.Exploring, GameStateEnum.InSettings}},
            { GameStateEnum.InSettings, new() {GameStateEnum.AtTitle, GameStateEnum.Exploring, GameStateEnum.Battling}},
        };

        public bool IsInBattle()
        {
            return new[] {GameStateEnum.Battling }.Contains(_gameState);
        }
        
        public void EnterBattling()
        {
            Debug.Log($"gameState = {_gameState}    ");

            if(_gameState != GameStateEnum.Exploring) return;
            ChangeState(GameStateEnum.Battling);
        }
        
        void ChangeState(GameStateEnum nextState)
        {
            Assert.IsTrue(_stateTransitionMap.ContainsKey(_gameState));  // Current State
            Assert.IsTrue(_stateTransitionMap[_gameState].Contains(nextState)); // Next State
            OnStart(_gameState);
            _gameState = nextState;
            OnEnd(_gameState);
        }
        
        void OnStart(GameStateEnum gameState)
        {
            switch (gameState)
            {
                case GameStateEnum.AtTitle:
                    // TitleScreen.SetActive(true);
                    break;
                case GameStateEnum.Exploring:
                    // Dungeon.SetActive(true);
                    break;
                case GameStateEnum.Battling:
                    // Battle.SetActive(true);
                    break;
                case GameStateEnum.InSettings:
                    // Settings.SetActive(true);
                    break;
            }
        }
        void OnEnd(GameStateEnum gameState)
        {
            switch (gameState)
            {
                case GameStateEnum.AtTitle:
                    // TitleScreen.SetActive(false);
                    break;
                case GameStateEnum.Exploring:
                    // Dungeon.SetActive(false);
                    break;
                case GameStateEnum.Battling:
                    // Battle.SetActive(false);
                    break;
                case GameStateEnum.InSettings:
                    // Settings.SetActive(false);
                    break;
            }
        }
    }
}
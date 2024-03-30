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
        public enum GameStateEnum
        {
            None,
            AtTitle,
            Exploring,
            Battling,
            InSettings,
            // InInventory,
        }
        public event EventHandler<GameStateEventArgs> OnGameStateChange = (sender, e) => { };

        GameStateEnum _gameState = GameStateEnum.None;
        
        readonly Dictionary<GameStateEnum, List<GameStateEnum>> _stateTransitionMap = new() // (Current State) -> (List of Possible Transition States)
        {
            { GameStateEnum.None, new() { GameStateEnum.None, GameStateEnum.AtTitle,GameStateEnum.Exploring, GameStateEnum.Battling, GameStateEnum.InSettings}}, 
            { GameStateEnum.AtTitle, new() { GameStateEnum.Exploring, GameStateEnum.InSettings}},
            { GameStateEnum.Exploring, new() { GameStateEnum.Battling, GameStateEnum.InSettings}},
            { GameStateEnum.Battling, new() { GameStateEnum.AtTitle, GameStateEnum.Exploring, GameStateEnum.InSettings}},
            { GameStateEnum.InSettings, new() {GameStateEnum.AtTitle, GameStateEnum.Exploring, GameStateEnum.Battling}},
        };
        public bool IsInTitle()
        {
            return new[] {GameStateEnum.AtTitle}.Contains(_gameState);
        }
        public void EnterTitle()
        {
            Debug.Log("EnterTitle()");
            ChangeState(GameStateEnum.AtTitle);
        }
        
        public bool IsInExploring()
        {
            return new[] {GameStateEnum.Exploring}.Contains(_gameState);
        }
        public void EnterExploring()
        {
            ChangeState(GameStateEnum.Exploring);
        }

        public bool IsInBattle()
        {
            return new[] {GameStateEnum.Battling }.Contains(_gameState);
        }
        
        public void EnterBattling()
        {
            Debug.Log($"gameState = {_gameState}    ");

            if(_gameState == GameStateEnum.Battling) return;
            ChangeState(GameStateEnum.Battling);
        }


        void ChangeState(GameStateEnum nextState)
        {
            Assert.IsTrue(_stateTransitionMap.ContainsKey(_gameState));  // Current State
            Assert.IsTrue(_stateTransitionMap[_gameState].Contains(nextState)); // Next State
            OnGameStateChange(this, new GameStateEventArgs(nextState));
            Debug.Log($"GameStateSwitcher.ChangeState() : {_gameState} -> {nextState}");
            _gameState = nextState;
        }
        
        
        public class GameStateEventArgs : EventArgs
        {
            public GameStateEnum NextGameState { get; }
            public GameStateEventArgs(GameStateEnum nextGameState)
            {
                NextGameState = nextGameState;
            }
        }
    }
}
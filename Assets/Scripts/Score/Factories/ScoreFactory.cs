using System;
using Score.Configs;
using Score.Views;
using UnityEngine;
using Zenject;

namespace Score.Factories
{
    public class ScoreFactory : IDisposable
    {
        private DiContainer _diContainer;
        private ScoreConfig _scoreConfig;
        private PlayerScoreBoard _playerScoreBoard;
        
        [Inject]
        public void Construct(DiContainer diContainer, ScoreConfig scoreConfig)
        {
            _diContainer = diContainer;
            _scoreConfig = scoreConfig;
        }

        public PlayerScoreView Create()
        {
            if (_playerScoreBoard == null)
            {
                _playerScoreBoard = _diContainer.InstantiatePrefabForComponent<PlayerScoreBoard>(_scoreConfig.PlayerScoreBoardPrefab);
            }
            var playerScore = _diContainer.InstantiatePrefabForComponent<PlayerScoreView>(_scoreConfig.Prefab,_playerScoreBoard.ScoreRoot);
            
            return playerScore;
        }

        public void Dispose()
        {
            if (_playerScoreBoard != null)
            {
                GameObject.Destroy(_playerScoreBoard.gameObject);
                _playerScoreBoard = null;
            }
        }
    }
}
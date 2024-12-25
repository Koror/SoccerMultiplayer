using Mirror;
using Player.Factory;
using Player.Views;
using UnityEngine;

namespace Player.Services
{
    public class PlayerService
    {
        private PlayerFactory _playerFactory;
        
        public PlayerService(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        public PlayerView Create(Vector3 position, Quaternion rotation)
        {
            var player = _playerFactory.Spawn(new SpawnMessage(){position = position, rotation = rotation});
            if (player.TryGetComponent(out PlayerView playerView))
            {
                return playerView;
            }
            return null;
        }
    }
}
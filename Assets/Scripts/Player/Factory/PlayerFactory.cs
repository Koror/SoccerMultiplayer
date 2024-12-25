using Mirror;
using Player.Config;
using Player.Views;
using UnityEngine;
using Zenject;

namespace Player.Factory
{
    public class PlayerFactory : IInitializable
    {
        private DiContainer _diContainer;
        private PlayerConfig _playerConfig;
        
        [Inject]
        public void Construct(DiContainer diContainer, PlayerConfig playerConfig)
        {
            _diContainer = diContainer;
            _playerConfig = playerConfig;
        }

        public PlayerView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.Prefab);
        }
        public void Initialize()
        {
            NetworkClient.RegisterPrefab(_playerConfig.Prefab.gameObject, Spawn, UnSpawn);
        }

        public GameObject Spawn(SpawnMessage msg)
        {
            return _diContainer.InstantiatePrefabForComponent<PlayerView>(_playerConfig.Prefab, msg.position, msg.rotation,null).gameObject;
        }

        public void UnSpawn(GameObject spawned)
        {
            GameObject.Destroy(spawned);
        }
    }
}
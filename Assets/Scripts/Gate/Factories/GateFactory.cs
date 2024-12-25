using Gate.Config;
using Gate.Views;
using Mirror;
using UnityEngine;
using Zenject;

namespace Gate.Factories
{
    public class GateFactory : IInitializable
    {
        private DiContainer _diContainer;
        private GateConfig _gateConfig;

        [Inject]
        public void Construct(DiContainer diContainer, GateConfig gateConfig)
        {
            _diContainer = diContainer;
            _gateConfig = gateConfig;
        }

        public void Initialize()
        {
            NetworkClient.RegisterPrefab(_gateConfig.Prefab.gameObject, Spawn, UnSpawn);
        }

        public GameObject Spawn(SpawnMessage msg)
        {
            return _diContainer.InstantiatePrefabForComponent<GateView>(_gateConfig.Prefab, msg.position, msg.rotation,null).gameObject;
        }

        public void UnSpawn(GameObject spawned)
        {
            GameObject.Destroy(spawned);
        }
    }
}
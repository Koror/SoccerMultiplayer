using Player.Config;
using Player.Views;
using Zenject;

namespace Player.Factory
{
    public class PlayerFactory
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
    }
}
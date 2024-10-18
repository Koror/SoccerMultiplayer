using Player.Factory;
using Player.Views;

namespace Player.Services
{
    public class PlayerService
    {
        private PlayerFactory _playerFactory;
        
        public PlayerService(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        public PlayerView Create()
        {
            return _playerFactory.Create();
        }
    }
}
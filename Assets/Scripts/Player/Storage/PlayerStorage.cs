using System.Collections.Generic;
using Player.Views;
using UniRx;

namespace Player.Storage
{
    public class PlayerStorage
    {
        private Dictionary<uint,PlayerView> _players = new ();

        public Subject<PlayerView> PlayerAdded = new();

        public void Add(uint id, PlayerView player)
        {
            _players.Add(id,player);
            PlayerAdded.OnNext(player);
        }

        public PlayerView Get(uint id)
        {
            if (_players.ContainsKey(id))
            {
                return _players[id];
            }

            return null;
        }

        public void Remove(uint id)
        {
            if (_players.ContainsKey(id))
            {
                _players.Remove(id);
            }
        }
            
   }
}
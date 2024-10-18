using System;
using System.Collections.Generic;
using Gate.Services;
using Mirror;
using Network.Config;
using Network.Data;
using Player.Services;
using Player.Views;
using UnityEngine;
using Zenject;

namespace Network.Managers
{
    public class NetworkManagerSoccer : NetworkManager
    {
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] private PlayerColorConfig _colorConfig;

        public Action OnConnected;
        public Action OnStartServerAction;
        public Action OnStartClientAction;

        private GateService _gateService;
        private PlayerService _playerService;
        
        private Dictionary<int, PlayerView> _players = new();
        
        [Inject]
        private void Construct(GateService gateService, PlayerService playerService)
        {
            _gateService = gateService;
            _playerService = playerService;
        }

        private void OnCreateCharacter(NetworkConnectionToClient conn, CreateCharacterMessage message)
        {
            if (spawnPositions.Count != 4)
            {
                Debug.Log("NetworkManagerSoccer: Spawn Position is missing");
                return;
            }

            //player
            var point = spawnPositions[numPlayers];
            var player = _playerService.Create();
            player.transform.position = point.position;
            player.transform.rotation = point.rotation;

            var color = _colorConfig.GetColorByType(message.Color);
            player.Initialize(color,message.Color.ToString(),numPlayers+1);
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
            _players.Add(numPlayers,player);
            
            //gate
            var gate = _gateService.Create(numPlayers,color);
            var gatePosition = point.position;
            gatePosition.y = 0;
            gate.transform.position = gatePosition;
            gate.transform.rotation = point.rotation;
            gate.Goal += SetGoal;
            NetworkServer.Spawn(gate.gameObject);
        }

        public override void OnServerConnect(NetworkConnectionToClient conn)
        {
            base.OnServerConnect(conn);
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            OnStartServerAction?.Invoke();
            NetworkServer.RegisterHandler<CreateCharacterMessage>(OnCreateCharacter); 
        }
        public override void OnStartClient()
        {
            OnStartClientAction?.Invoke();
        }

        public override void OnClientConnect()
        {
            base.OnClientConnect();
            OnConnected?.Invoke();
        }
        
        [Server]
        private void SetGoal(int playerGoal,int playerGate)
        {
            _players[playerGoal].AddScore(1);
            _players[playerGate].AddScore(-1);
        }
    }
}
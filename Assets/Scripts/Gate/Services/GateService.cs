using Gate.Config;
using Gate.Factories;
using Gate.Views;
using Mirror;
using UnityEngine;
using Zenject;

namespace Gate.Services
{
    public class GateService
    {
        private GateFactory _gateFactory;
        private GateConfig _gateConfig;

        public GateService(GateFactory gateFactory, GateConfig gateConfig)
        {
            _gateFactory = gateFactory;
            _gateConfig = gateConfig;
        }



        public GateView Create(int playerId, Color color, Vector3 position, Quaternion rotation)
        {
            var gate = _gateFactory.Spawn(new SpawnMessage(){ position = position,rotation = rotation});
            if(gate.TryGetComponent(out GateView gateView))
            {
                gateView.Initialize(playerId, color,_gateConfig.Speed,_gateConfig.Distance);
                return gateView;
            }
            return null;
        }
    }
}
using Gate.Config;
using Gate.Factories;
using Gate.Views;
using UnityEngine;

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

        public GateView Create(int playerId, Color color)
        {
            var gate = _gateFactory.Create();
            gate.Initialize(playerId, color,_gateConfig.Speed,_gateConfig.Distance);
            return gate;
        }
    }
}
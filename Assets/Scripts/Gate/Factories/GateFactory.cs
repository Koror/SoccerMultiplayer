using Gate.Config;
using Gate.Views;
using Zenject;

namespace Gate.Factories
{
    public class GateFactory
    {
        private DiContainer _diContainer;
        private GateConfig _gateConfig;
        
        [Inject]
        public void Construct(DiContainer diContainer, GateConfig gateConfig)
        {
            _diContainer = diContainer;
            _gateConfig = gateConfig;
        }

        public GateView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<GateView>(_gateConfig.Prefab);
        }
    }
}
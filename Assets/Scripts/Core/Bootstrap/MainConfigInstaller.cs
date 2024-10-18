using Gate.Config;
using Player.Config;
using Score.Configs;
using UnityEngine;
using Utils;
using Zenject;

namespace Core.Bootstrap
{
    public class MainConfigInstaller : MonoInstaller
    {
        [SerializeField] private GateConfig _gateConfig;
        [SerializeField] private ScoreConfig _scoreConfig;
        [SerializeField] private PlayerConfig _playerConfig;

        public override void InstallBindings()
        {
            Container.InstallConfig(_gateConfig);
            Container.InstallConfig(_scoreConfig);
            Container.InstallConfig(_playerConfig);
        }
    }
}
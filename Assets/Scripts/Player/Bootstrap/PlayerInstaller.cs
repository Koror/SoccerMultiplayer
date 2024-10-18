using Player.Factory;
using Player.Services;
using Zenject;

namespace Player.Bootstrap
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<PlayerService>().AsSingle().NonLazy();
        }
    }
}
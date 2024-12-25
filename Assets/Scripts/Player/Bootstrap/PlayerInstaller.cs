using Player.Factory;
using Player.Services;
using Player.Storage;
using Zenject;

namespace Player.Bootstrap
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerStorage>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerFactory>().AsSingle().NonLazy();
            Container.Bind<PlayerService>().AsSingle().NonLazy();
        }
    }
}
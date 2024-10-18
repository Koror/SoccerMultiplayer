using System.ComponentModel;
using Score.Factories;
using Score.Services;
using Zenject;

namespace Score.Bootstrap
{
    public class ScoreInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreFactory>().AsSingle().NonLazy();
            Container.Bind<ScoreService>().AsSingle().NonLazy();
        }
    }
}
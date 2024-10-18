using Gate.Bootstrap;
using Player.Bootstrap;
using Score.Bootstrap;
using Zenject;

namespace Core.Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<ScoreInstaller>();
            Container.Install<PlayerInstaller>();
            Container.Install<GateInstaller>();
        }
    }
}
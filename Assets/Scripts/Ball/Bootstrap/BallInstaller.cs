using Ball.Pool;
using UnityEngine;
using Zenject;

namespace Ball.Bootstrap
{
    public class BallInstaller : MonoInstaller
    {
        [SerializeField] private BallPool _ballPool;
        
        public override void InstallBindings()
        {
            Container.Bind<BallPool>()
                .FromComponentInNewPrefab(_ballPool)
                .AsSingle()
                .NonLazy();
        }
    }
}
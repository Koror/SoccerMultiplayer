using Zenject;

namespace Utils
{
    public static class ZenjectExtensions
    {
        public static void InstallConfig<TRegistry>(this DiContainer container, TRegistry registry)
        {
            container
                .BindInterfacesAndSelfTo<TRegistry>()
                .FromInstance(registry)
                .AsSingle()
                .NonLazy();
        }
    }
}
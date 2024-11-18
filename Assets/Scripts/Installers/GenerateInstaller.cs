using UnityEngine;
using Zenject;

public class GenerateInstaller : MonoInstaller
{
    [SerializeField] private MapSettings _mapSettings;

    public override void InstallBindings()
    {
        Container.Bind<ChunkFactory>().AsSingle();
        Container.Bind<MobFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<MapDataConfigurator>().FromNew().AsSingle().WithArguments(_mapSettings);
    }
}

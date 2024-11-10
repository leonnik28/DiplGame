using Zenject;

public class GenerateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ChunkFactory>().AsSingle();
        Container.Bind<MobFactory>().AsSingle();
        Container.Bind<MapSaver>().AsSingle();
    }
}

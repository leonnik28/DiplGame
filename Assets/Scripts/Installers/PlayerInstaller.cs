using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Bag>().AsSingle();
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
    }
}

using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Bag>().FromNew().AsSingle();
    }
}

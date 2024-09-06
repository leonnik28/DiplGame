using Zenject;

public class StorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StorageService>().FromNew().AsSingle();
    }
}

using Zenject;

public class ResourceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ResourcePresenter>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ResourceFactory>().FromNew().AsSingle();
    }
}

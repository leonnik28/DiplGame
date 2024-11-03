using Zenject;

public class DataInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<UserData>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<GameSession>().FromNew().AsSingle();
    }
}

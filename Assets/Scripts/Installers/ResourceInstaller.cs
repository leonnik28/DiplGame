using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceInstaller : MonoInstaller
{
    [SerializeField] List<BaseResourceSettings> _settings;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ResourcePresenter>().AsSingle().WithArguments(_settings, Container);
        Container.BindInterfacesAndSelfTo<ResourceFactory>().FromNew().AsSingle();
    }
}

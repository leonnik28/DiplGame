using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ResourceInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ItemPresenter>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<ResourceFactory>().FromNew().AsSingle();
    }
}

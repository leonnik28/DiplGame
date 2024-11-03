using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bag
{
    private readonly List<BaseResource> _resourcesInBag = new List<BaseResource>();

    private GameSession _gameSession;

    [Inject]
    private void Construct(GameSession gameSession)
    {
        _gameSession = gameSession;
    }

    public void GetResource(BaseResource resource)
    {
        _resourcesInBag.Add(resource);
        Debug.Log(resource.ToString());
    }

    public void SaveResources()
    {
        _ = _gameSession.SaveData(_resourcesInBag);
    }
}

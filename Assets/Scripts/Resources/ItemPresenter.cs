using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ItemPresenter : MonoBehaviour
{
    [SerializeField] private List<ItemSettings> _settings;

    private List<ItemResource> resources = new List<ItemResource>();

    [Inject]
    private DiContainer _container;

    private void Awake()
    {
        foreach (var item in _settings)
        {
            resources.Add(_container.Instantiate<ItemResource>(new object[] { item }));
        }
    }

    public async UniTaskVoid SpawnItem(Vector3 position, BaseResourceSettings baseResourceSettings = null)
    {
        ItemResource targetResource = resources.FirstOrDefault(r => r.ItemSettings == baseResourceSettings);

        if (targetResource != null)
        {
            await targetResource.Spawn(targetResource.ItemSettings.GameObject, position);
        }
    }
}

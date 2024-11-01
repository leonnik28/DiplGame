using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour
{
    [SerializeField] private ChestSettings _settings;

    private ItemPresenter _presenter;
    [Inject]
    private void Construct(ItemPresenter presenter)
    {
        _presenter = presenter;
    }

    private void Start()
    {
        Destroy(gameObject);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        foreach(BaseResourceSettings resource in _settings.Resources)
        {
            _presenter.SpawnItem(gameObject.transform.position ,resource);
        }
    }
}

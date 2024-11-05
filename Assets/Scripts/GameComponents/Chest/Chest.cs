using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour
{
    [SerializeField] private ChestSettings _settings;

    private ResourcePresenter _presenter;
    [Inject]
    private void Construct(ResourcePresenter presenter)
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
        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1;
        foreach (BaseResourceSettings resource in _settings.Resources)
        {
            _presenter.SpawnItem(spawnPosition, resource);
        }
    }
}

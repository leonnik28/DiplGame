using UnityEngine;
using Zenject;

public class Chest : MonoBehaviour, IDamageble
{
    [SerializeField] private ChestSettings _settings;

    private int _healt;

    private ResourcePresenter _presenter;
    [Inject]
    private void Construct(ResourcePresenter presenter)
    {
        _presenter = presenter;
    }

    private void Start()
    {
        _healt = _settings.Healt;
    }

    public void TakeDamage(int damage)
    {
        _healt -= damage;
        if (_healt <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);

        Vector3 spawnPosition = transform.position;
        spawnPosition.y += 1;
        foreach (BaseResourceSettings resource in _settings.Resources)
        {
            _presenter.SpawnItem(spawnPosition, resource);
        }
    }
}

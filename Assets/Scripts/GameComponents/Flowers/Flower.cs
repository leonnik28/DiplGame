using System.Collections;
using UnityEngine;
using Zenject;

public class Flower : MonoBehaviour
{
    [SerializeField] private Timer _timer;
    [SerializeField] private FlowerSettings _settings;

    private bool _isPicking;
    private Coroutine _pickCoroutine;

    private bool _isActiveFlower = true;

    private Bag _bag;

    [Inject]
    private void Construct(Bag bag)
    {
        _bag = bag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActiveFlower && other.TryGetComponent(out Player player))
        {
            float timeToCollect = _settings.TimeToCollect / player.CountBee;
            StartPicking(player, timeToCollect);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_isActiveFlower && other.TryGetComponent(out Player _))
        {
            StopPicking();
        }
    }

    private void Complete()
    {
        _timer.gameObject.SetActive(false);
        _isActiveFlower = false;
    }

    private void StartPicking(Player player, float timeToCollect)
    {
        _pickCoroutine ??= StartCoroutine(PickCoroutine(player, timeToCollect));
    }

    private void StopPicking()
    {
        if (_pickCoroutine != null)
        {
            StopCoroutine(_pickCoroutine);
            _pickCoroutine = null;
            _isPicking = false;
            _timer.gameObject.SetActive(false);
        }
    }

    private IEnumerator PickCoroutine(Player player, float timeToCollect)
    {
        _isPicking = true;
        _timer.gameObject.SetActive(true);
        _timer.StartTimer(timeToCollect);
        float startTime = Time.time;

        while (_isPicking && Time.time - startTime < timeToCollect)
        {
            yield return null;
        }

        if (_isPicking)
        {
            Complete();
        }
        
        _isPicking = false;
        _pickCoroutine = null;
    }
}

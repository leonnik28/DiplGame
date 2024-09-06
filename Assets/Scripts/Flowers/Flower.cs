using System.Collections;
using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private FlowerSettings _settings;

    private bool _isPicking;
    private Coroutine _pickCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            float timeToCollect = _settings.TimeToCollect / player.CountBee;
            StartPicking(timeToCollect);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            StopPicking();
        }
    }

    private void StartPicking(float timeToCollect)
    {
        if (_pickCoroutine == null)
        {
            _pickCoroutine = StartCoroutine(PickCoroutine(timeToCollect));
        }
    }

    private void StopPicking()
    {
        if (_pickCoroutine != null)
        {
            StopCoroutine(_pickCoroutine);
            _pickCoroutine = null;
            _isPicking = false;
        }
    }

    private IEnumerator PickCoroutine(float timeToCollect)
    {
        _isPicking = true;
        float startTime = Time.time;

        while (_isPicking && Time.time - startTime < timeToCollect)
        {
            yield return null;
        }

        if (_isPicking)
        {
            Debug.Log("Flower picked!");
        }
        // прогресс завершен
        _isPicking = false;
        _pickCoroutine = null;
    }
}

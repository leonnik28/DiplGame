using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

[RequireComponent(typeof(Collider))]
public class UITriggerHandler : MonoBehaviour
{
    [SerializeField] private AssetReference _uiReference;

    private GameObject _instantiatedObject;
    private DiContainer _container;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _uiReference.InstantiateAsync().Completed += OnInstantiateCompleted;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_instantiatedObject != null)
            {
                Destroy(_instantiatedObject);
                _instantiatedObject = null;
            }
        }
    }

    private void OnInstantiateCompleted(AsyncOperationHandle<GameObject> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _instantiatedObject = handle.Result;
            _container.Inject(_instantiatedObject.GetComponent<PlayNextScene>());
        }
        else
        {
            Debug.LogError("Failed to instantiate UI object.");
        }
    }
}

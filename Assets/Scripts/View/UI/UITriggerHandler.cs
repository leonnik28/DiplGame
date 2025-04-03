using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

[RequireComponent(typeof(Collider))]
public class UITriggerHandler : MonoBehaviour
{
    [SerializeField] private AssetReference _uiReference;

    private UnityEngine.GameObject _instantiatedObject;
    private DiContainer _container;
    private AsyncOperationHandle<UnityEngine.GameObject> _preloadedHandle;

    [Inject]
    private void Construct(DiContainer container)
    {
        _container = container;
    }

    private void Awake()
    {
        PreloadAsset();
    }

    private void OnDestroy()
    {
        if (_preloadedHandle.IsValid())
        {
            Addressables.Release(_preloadedHandle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            if (_preloadedHandle.IsDone)
            {
                _instantiatedObject = Instantiate(_preloadedHandle.Result);
                _container.Inject(_instantiatedObject.GetComponent<PlayNextScene>());
            }
            else
            {
                Debug.LogError("UI asset is not preloaded yet.");
            }
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

    private void PreloadAsset()
    {
        _preloadedHandle = _uiReference.LoadAssetAsync<UnityEngine.GameObject>();
    }
}

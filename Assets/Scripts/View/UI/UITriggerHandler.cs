using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(Collider))]
public class UITriggerHandler : MonoBehaviour
{
    [SerializeField] private AssetReference _uiReference;

    private GameObject _instantiatedObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _uiReference.InstantiateAsync().Completed += handle =>
            {
                _instantiatedObject = handle.Result;
            };
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
}

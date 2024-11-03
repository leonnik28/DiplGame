using UnityEngine;
using Zenject;

public class Home : MonoBehaviour
{
    [SerializeField] private GameObject _homeUI;

    private Bag _bag;

    [Inject]
    private void Construct(Bag bag)
    {
        _bag = bag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _bag.FoldMoney();
            _homeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _homeUI.SetActive(false);
        }
    }
}

using UnityEngine;
using Zenject;

public class Home : MonoBehaviour
{
    private Bag _bag;

    [Inject]
    private void Construct(Bag bag)
    {
        _bag = bag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetComponent(out Player _))
        {
            _bag.FoldMoney();
        }
    }
}

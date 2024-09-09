using UnityEngine;

public class Bee : MonoBehaviour
{
    private GameObject _mainBee;

    private void Awake()
    {
        _mainBee = GameObject.FindWithTag("MainBee");
    }

    private void Update()
    {
        if (_mainBee != null)
        {
            transform.rotation = _mainBee.transform.rotation;
        }
    }
}

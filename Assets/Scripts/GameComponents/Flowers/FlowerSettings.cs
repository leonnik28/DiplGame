using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Flower/Create New Flower")]
public class FlowerSettings : ScriptableObject
{
    public int MoneyForCollection => _moneyForCollection;
    public float TimeToCollect => _timeToCollect;

    [SerializeField] private int _moneyForCollection;
    [SerializeField] private float _timeToCollect;
}

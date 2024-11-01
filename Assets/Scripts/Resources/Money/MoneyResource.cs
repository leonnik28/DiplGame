using Cysharp.Threading.Tasks;
using UnityEngine;

public class MoneyResource : BaseResource
{
    [SerializeField] private MoneySettings _settings;

    public override async UniTask Spawn(GameObject gameObject, Vector3 position)
    {
        await base.Spawn(gameObject, position);
    }

    protected override void Collect()
    {
        
    }

}

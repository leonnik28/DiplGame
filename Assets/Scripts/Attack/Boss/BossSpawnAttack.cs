using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnAttack : BaseAttack
{
    private bool _stop = false;
    private MobFactory _mobFactory;
    private List<UnityEngine.GameObject> _mobsPrefabs;

    public BossSpawnAttack(AttackSettings attackSettings, Animator animator, MobFactory mobFactory, List<UnityEngine.GameObject> mobsPrefabs) : base(attackSettings, animator)
    {
        _mobFactory = mobFactory;
        _mobsPrefabs = mobsPrefabs;
    }

    public async void SpawnAttack()
    {
        if (!IsCooldown())
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("spawnEnemies");
            await DelayAttack();
            if (!_stop)
            {
                int mobCount = UnityEngine.Random.Range(1, 5);
                for (int i = 0; i < mobCount; i++)
                {
                    Vector3 spawnPosition = GetRandomPositionInCircle(_animator.gameObject.transform.position, _attackSettings.Range);
                    UnityEngine.GameObject randomMobPrefab = _mobsPrefabs[UnityEngine.Random.Range(0, _mobsPrefabs.Count)];
                    _mobFactory.Create(randomMobPrefab, spawnPosition);
                }
            }
        }
    }

    public void StopAttack()
    {
        _stop = true;
    }

    private async UniTask DelayAttack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(10f));
    }

    private Vector3 GetRandomPositionInCircle(Vector3 center, float radius)
    {
        float angle = UnityEngine.Random.Range(0f, 360f);
        float distance = UnityEngine.Random.Range(0f, radius);
        float x = center.x + distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = center.z + distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector3(x, center.y, z); // y координата остается постоянной
    }
}

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class BossMeleeAttack : BaseAttack
{
    private float _lastAttackTime = 0f;
    private readonly float _damageCoefficient = 2f;
    private readonly float _rangeCoefficient = 1.5f;
    private bool _stop = false;

    public BossMeleeAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override async void Attack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            await DelayAttack();
            if (!_stop)
            {
                _animator.SetTrigger("attack");
                Collider[] hitPlayer = Physics.OverlapSphere(transform.position, _attackSettings.Range);
                foreach (Collider hit in hitPlayer)
                {
                    if (hit.TryGetComponent<Player>(out Player player) && IsInCone(hit.transform.position, transform))
                    {
                        player.TakeDamage(_attackSettings.Damage);
                    }
                }
            }
        }
    }

    public async void AreaAttack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            await DelayAttack();
            if (!_stop)
            {
                _animator.SetTrigger("areaAttack");
                Collider[] hitPlayer = Physics.OverlapSphere(transform.position, _attackSettings.Range);
                foreach (Collider hit in hitPlayer)
                {
                    if (hit.TryGetComponent<Player>(out Player player))
                    {
                        player.TakeDamage(_attackSettings.Damage);
                    }
                }
            }
        }
    }

    public async void ChargedAttack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("charge");
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            if (!_stop)
            {
                _animator.SetTrigger("chargedAttack");
                float increasedRange = _attackSettings.Range * _rangeCoefficient;
                int increasedDamage = (int)(_attackSettings.Damage * _damageCoefficient);
                Collider[] hitPlayer = Physics.OverlapSphere(transform.position, increasedRange);
                foreach (Collider hit in hitPlayer)
                {
                    if (hit.TryGetComponent<Player>(out Player player) && IsInCone(hit.transform.position, transform))
                    {
                        player.TakeDamage(increasedDamage);
                    }
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
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
    }
}

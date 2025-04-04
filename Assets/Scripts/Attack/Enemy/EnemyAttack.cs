using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemyAttack : BaseAttack
{
    private bool _stop = false;

    public EnemyAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override async void Attack(Transform transform)
    {
        if (!IsCooldown())
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

    public void StopAttack()
    {
        _stop = true;
    }

    private async UniTask DelayAttack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }
}

using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class EnemyAttack : BaseAttack
{
    private float _lastAttackTime = 0f;

    public EnemyAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override async void Attack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            await DelayAttack();
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

    private async UniTask DelayAttack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1));
    }
}

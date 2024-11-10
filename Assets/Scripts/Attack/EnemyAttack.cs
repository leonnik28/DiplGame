using UnityEngine;

public class EnemyAttack : BaseAttack
{
    private float _lastAttackTime = 0f;

    public EnemyAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override void Attack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;

            _animator.SetTrigger("attack");

            Collider[] hitPlayer = Physics.OverlapSphere(transform.position, _attackSettings.Range);
            foreach (Collider hit in hitPlayer)
            {
                Debug.Log(hit);
                if (hit.TryGetComponent<Player>(out Player player) && IsInCone(hit.transform.position, transform))
                {
                   // player.TakeDamage(_attackSettings.Damage);
                }
            }
        }

    }
}

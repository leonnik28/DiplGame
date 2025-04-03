using UnityEngine;

public class PlayerAttack : BaseAttack
{
    public PlayerAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override void Attack(Transform transform)
    {
        if (!IsCooldown())
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("attack");

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, _attackSettings.Range);
            foreach (Collider hitEnemy in hitEnemies)
            {
                Debug.Log(hitEnemy);
                if (hitEnemy.TryGetComponent<IDamageble>(out IDamageble enemy) && IsInCone(hitEnemy.transform.position, transform))
                {
                    if (enemy.GetType() != typeof(Player))
                    {
                        enemy.TakeDamage(_attackSettings.Damage);
                    }
                }
            }
        }
    }
}

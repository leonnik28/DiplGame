using UnityEngine;

public class PlayerAttack : BaseAttack
{
    private float _lastAttackTime = 0f;

    public PlayerAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings, animator) { }

    public override void Attack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("attack");

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, _attackSettings.Range);
            foreach (Collider hitEnemy in hitEnemies)
            {
                Debug.Log(hitEnemy);
                if (hitEnemy.TryGetComponent<Enemy>(out Enemy enemy) && IsInCone(hitEnemy.transform.position, transform))
                {
                    enemy.TakeDamage(_attackSettings.Damage);
                }
            }
        }
    }
}

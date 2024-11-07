using UnityEngine;

public class PlayerAttack : BaseAttack
{
    [SerializeField] private Animator _animator;

    private float _lastAttackTime = 0f;

    public PlayerAttack(AttackSettings attackSettings, Animator animator) : base(attackSettings)
    {
        _attackSettings = attackSettings;
        _animator = animator;
    }

    public override void Attack(Transform transform)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("attack");

            Collider[] hitEnemies = Physics.OverlapSphere(transform.position, _attackSettings.Range);
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy") && IsInCone(enemy.transform.position, transform))
                {
                    //  enemy.GetComponent<EnemyHealth>().TakeDamage(_damage);
                }
            }
        }
    }
}

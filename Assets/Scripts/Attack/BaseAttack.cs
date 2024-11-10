using UnityEngine;

public abstract class BaseAttack
{
    [SerializeField] protected AttackSettings _attackSettings;
    [SerializeField] protected Animator _animator;

    public BaseAttack(AttackSettings attackSettings, Animator animator)
    {
        _attackSettings = attackSettings;
        _animator = animator;
    }

    public virtual void Attack(Transform transform) { }

    protected bool IsInCone(Vector3 targetPosition, Transform transform)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float horizontalAngle = Vector3.Angle(transform.forward, directionToTarget);
        float verticalAngle = Vector3.Angle(transform.up, directionToTarget);

        return horizontalAngle <= _attackSettings.Angle / 2f && verticalAngle <= 90f;
    }

}

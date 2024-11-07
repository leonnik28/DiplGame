using UnityEngine;

public abstract class BaseAttack
{
    [SerializeField] protected AttackSettings _attackSettings;

    public BaseAttack(AttackSettings attackSettings)
    {
        _attackSettings = attackSettings;
    }

    public virtual void Attack(Transform transform) { }

    protected bool IsInCone(Vector3 targetPosition, Transform transform)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        return angleToTarget <= _attackSettings.Angle / 2f;
    }
}

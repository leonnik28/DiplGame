using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class BossRangedAttack : BaseAttack
{
    private float _lastAttackTime = 0f;
    private bool _stop = false;

    private readonly Transform _bossPosition;
    private readonly float _projectileSpeed;

    public BossRangedAttack(AttackSettings attackSettings, Animator animator, Transform bossPosition, float projectileSpeed) : base(attackSettings, animator) 
    {
        _bossPosition = bossPosition;
        _projectileSpeed = projectileSpeed;
    }

    public override async void Attack(Transform target)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("pistolAttack");
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            if (!_stop)
            {
                Vector3 targetPosition = target.position;
                Vector3 direction = (targetPosition - _bossPosition.position).normalized;
                RaycastHit hit;

                if (Physics.Raycast(_bossPosition.position, direction, out hit, _attackSettings.Range * 5f))
                {
                    if (hit.collider.TryGetComponent<Player>(out Player player))
                    {
                        player.TakeDamage(_attackSettings.Damage);
                    }
                }
            }
        }
    }

    public async void CatapultAttack(Transform target, GameObject catapultObject , bool multipleAttack)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("catapultAttack");
            await DelayAttack();
            if (!_stop)
            {
                if (multipleAttack)
                {
                    LaunchMultipleProjectiles(target, catapultObject, true);
                }
                else
                {
                    Vector3 launchDirection = (target.position - _bossPosition.position).normalized;
                    GameObject projectile = GameObject.Instantiate(catapultObject, _bossPosition.position, Quaternion.identity);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    rb.velocity = launchDirection * _projectileSpeed + Vector3.up * _projectileSpeed;
                    await CheckForImpact(projectile);
                }
            }
        }
    }

    public async void DirectProjectileAttack(Transform target, GameObject bulletObject , bool multipleAttack)
    {
        if (Time.time - _lastAttackTime >= _attackSettings.CooldownTime)
        {
            _lastAttackTime = Time.time;
            _animator.SetTrigger("directAttack");
            await DelayAttack();
            if (!_stop)
            {
                if (multipleAttack)
                {
                    LaunchMultipleProjectiles(target, bulletObject, false);
                }
                else
                {
                    Vector3 launchDirection = (target.position - _bossPosition.position).normalized;
                    GameObject projectile = GameObject.Instantiate(bulletObject, _bossPosition.position, Quaternion.identity);
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    rb.velocity = launchDirection * _projectileSpeed;
                    await CheckForImpact(projectile);
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

    private async UniTask CheckForImpact(GameObject projectile)
    {
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        await UniTask.WaitUntil(() => rb.velocity.magnitude < 0.1f);
        Explode(projectile.transform.position);
        GameObject.Destroy(projectile);
    }

    private void Explode(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, _attackSettings.Range);
        foreach (Collider hit in hitColliders)
        {
            if (hit.TryGetComponent<Player>(out Player player))
            {
                player.TakeDamage(_attackSettings.Damage);
            }
        }
    }

    private void LaunchMultipleProjectiles(Transform target, GameObject bulletPrefab, bool isCatapult)
    {
        int numberOfProjectiles = UnityEngine.Random.Range(2, 7);

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector3 randomDirection = UnityEngine.Random.onUnitSphere;
            GameObject projectile = GameObject.Instantiate(bulletPrefab, _bossPosition.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            if (isCatapult)
            {
                rb.velocity = randomDirection * _projectileSpeed + Vector3.up * _projectileSpeed;
            }
            else
            {
                rb.velocity = randomDirection * _projectileSpeed;
            }

            CheckForImpact(projectile).Forget();
        }
    }
}

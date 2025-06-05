using UnityEngine;

public class ArcherProjectile : BaseProjectile
{
    protected override void HitTarget()
    {
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        base.HitTarget();
    }
}

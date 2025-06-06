using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FortressProjectile : BaseProjectile
{
    protected override void HitTarget()
    {
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        base.HitTarget();
    }
}

using UnityEngine;

public class MageProjectile : BaseProjectile
{
    protected override void HitTarget()
    {
        target.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        base.HitTarget();        
    }

}

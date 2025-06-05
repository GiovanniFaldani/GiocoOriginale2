using UnityEngine;

public class AlchemistProjectile : BaseProjectile
{
    public DamageOvertime Dot_Ref;

    private void Start()
    {
        Dot_Ref.DotDamage = damage;
    }
    // Colpisce il bersaglio 
    protected override void HitTarget()
    {
        Dot_Ref.StartEffect();
        base.HitTarget();
    }

    public override void ActivateProjectile(Transform _target)
    {
        base.ActivateProjectile(_target);
        Dot_Ref = target.gameObject.GetComponent<DamageOvertime>();
    }
}

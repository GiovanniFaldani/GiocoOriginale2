using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed; // Velocità del proiettile
    public float damage; // Danno inflitto al bersaglio
    public bool bIsActive;

    private Transform target; // Bersaglio da inseguire
    public Transform resetPosition;

    void Update()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        float frameDistance = speed * Time.deltaTime;

        if (direction.magnitude <= frameDistance)
        {
            HitTarget();
        }
        transform.Translate(direction * frameDistance, Space.World);
    }


    // Colpisce il bersaglio
    void HitTarget()
    {
        DeactivateProjectile();
        //target.GetComponent<EnemyHealth>().TakeDamage(damage);
    }


    public void ActivateProjectile(Transform _target)
    { 
        gameObject.SetActive(true);
        bIsActive = true;
        target = _target;
    }

    public void DeactivateProjectile()
    {
        gameObject.SetActive(false);
        transform.position = resetPosition.position;
        bIsActive = false;
    }
}

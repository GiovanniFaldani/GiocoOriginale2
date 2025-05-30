using System.Runtime.InteropServices;
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
        Vector3 totalMovement = target.position - transform.position;
        float distance = totalMovement.magnitude;
        
        if (distance <= 1)
        {
            HitTarget();
        }
        transform.Translate(totalMovement.normalized * speed * Time.deltaTime, Space.World);
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

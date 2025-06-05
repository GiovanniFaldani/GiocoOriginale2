using System.Runtime.InteropServices;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float speed; // Velocità del proiettile
    public float damage; // Danno inflitto al bersaglio
    public bool bIsActive; // bool per controllo proiettile attivo in scena
    public float bulletLifetime;
    Vector3 totalMovement;

    protected Transform target; // Bersaglio da inseguire
    public Transform resetPosition;

    void Update()
    {
        bulletLifetime -= Time.deltaTime;

        if (target != null && bulletLifetime > 0)
        {
            MoveProjectile();
        }
        else
        { 
            DeactivateProjectile();
        }        
    }

    public void MoveProjectile()
    {
        totalMovement = target.position - transform.position;
        float distance = totalMovement.magnitude;

        if (distance <= 1)
        {
            HitTarget();
        }
        transform.Translate(totalMovement.normalized * speed * Time.deltaTime, Space.World);

    }

    // Colpisce il bersaglio
    protected virtual void HitTarget()
    {
        DeactivateProjectile();
    }


    public virtual void ActivateProjectile(Transform _target)
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

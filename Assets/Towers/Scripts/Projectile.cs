using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target; // Bersaglio da inseguire
    public float speed = 10f; // Velocità del proiettile
    public float damage = 20f; // Danno inflitto al bersaglio
    public Transform resetPosition;

    public bool bIsActive;

    void Update()
    {
        // Direzione verso il bersaglio
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Se è abbastanza vicino, colpisce
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }

        // Altrimenti si muove verso il bersaglio
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Colpisce il bersaglio
    void HitTarget()
    {
        DeactivateProjectile();
        //TODO: INSERIRE FUNZIONE DANNO AL NEMICO
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
        bIsActive = false;
        transform.position = resetPosition.position;

    }
}

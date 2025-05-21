using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target; // Bersaglio da inseguire
    public float speed = 10f; // Velocità del proiettile
    public float damage = 20f; // Danno inflitto al bersaglio

    // Imposta il bersaglio da colpire
    public void Seek(Transform _target)
    {
        target = _target;
    }

    void Update()
    {
        if (target == null) return;

        // Direzione verso il bersaglio
        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Se è abbastanza vicino, colpisce
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        // Altrimenti si muove verso il bersaglio
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    // Colpisce il bersaglio
    void HitTarget()
    {
        Destroy(gameObject);
    }
}

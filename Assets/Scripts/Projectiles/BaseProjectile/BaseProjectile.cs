using System.Runtime.InteropServices;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    public float speed; // Velocità del proiettile
    public float damage; // Danno inflitto al bersaglio
    public bool bIsActive; // bool per controllo proiettile attivo in scena

    private Transform target; // Bersaglio da inseguire
    public Transform resetPosition;
    public Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

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
        enemy.TakeDamage(damage);
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

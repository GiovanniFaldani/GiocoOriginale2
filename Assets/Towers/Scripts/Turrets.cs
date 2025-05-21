using UnityEngine;
using System.Collections.Generic;  

public class Turrets : MonoBehaviour
{
    [Header("Comportamento di fuoco")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireRate = 1f;

    private float fireTimer;
    private List<Transform> targetsInRange = new List<Transform>();

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        CleanNullTargets();

        if (targetsInRange.Count > 0 && fireTimer <= 0f)
        {
            Debug.Log($"[Torretta] Sparo a: {targetsInRange[0].name}");

            ShootAt(targetsInRange[0]);
            fireTimer = 1f / fireRate;
        }
    }

    private void ShootAt(Transform target)
    {
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        proj.GetComponent<Projectile>().Seek(target);

        Debug.Log($"[Torretta] Proiettile istanziato verso: {target.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.transform);
            Debug.Log($"[Torretta] Nemico entrato nel raggio: {other.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
            Debug.Log($"[Torretta] Nemico uscito dal raggio: {other.name}");
        }
    }

    private void CleanNullTargets()
    {
        int countBefore = targetsInRange.Count;
        targetsInRange.RemoveAll(t => t == null);
        int countAfter = targetsInRange.Count;

        if (countBefore != countAfter)
        {
            Debug.Log($"[Torretta] Rimosso {countBefore - countAfter} bersaglio/i nullo/i dalla lista.");
        }
    }

    private void OnDrawGizmosSelected()
    { 
        if (targetsInRange.Count > 0 && targetsInRange[0] != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(firePoint.position, targetsInRange[0].position);
        }
    }
}

using UnityEngine;
using System.Collections.Generic;  

public class BaseTower : MonoBehaviour
{
    // Punto da cui parte il proiettile
    [SerializeField] public Transform firePoint;

    // Numero di colpi al secondo che la torretta può sparare
    [SerializeField] public float fireRate = 1f;

    // Tempo (in secondi) dopo il quale la torretta può sparare di nuovo
    private float nextFireTime = 0f;

    // Lista dei bersagli attualmente nel raggio d’azione
    public List<Transform> targetsInRange = new List<Transform>();

    public Projectile_Pool pool;

    private void Start()
    {
        pool = GetComponent<Projectile_Pool>();
    }

    private void Update()
    {
        

        // Se c'è almeno un bersaglio e siamo oltre il tempo di ricarica...
        if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
        {
            // Rimuove dalla lista eventuali bersagli nulli (es. distrutti)
            CleanNullTargets();

            // Spara al primo bersaglio nella lista
            ShootAt(targetsInRange[0]);

            // Imposta il prossimo momento in cui la torretta può sparare
            nextFireTime = Time.time + (1f / fireRate);
        }
    }


    // Crea un proiettile e lo invia verso il bersaglio
    private void ShootAt(Transform target)
    {
        pool.ChooseProjectile().ActivateProjectile(target);
    }

    // Aggiunge un nemico alla lista quando entra nel trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.transform);
        }
    }

    // Rimuove il nemico dalla lista quando esce dal trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
        }
    }

    // Pulisce la lista dei bersagli
    private void CleanNullTargets()
    {
        int countBefore = targetsInRange.Count;

        // Creiamo una nuova lista per i bersagli validi (non null)
        List<Transform> validTargets = new List<Transform>();

        // Controlliamo uno per uno gli elementi nella lista
        foreach (Transform t in targetsInRange)
        {
            if (t != null && !t.gameObject.GetComponent<Enemy>().HP.IsDead)
            {
                validTargets.Add(t); // aggiungiamo solo quelli validi
            }
        }

        // Sostituiamo la lista originale con quella pulita
        targetsInRange = validTargets;

        int countAfter = targetsInRange.Count;

    }

    // debug range
    private void OnDrawGizmosSelected()
    {
        if (targetsInRange.Count > 0 && targetsInRange[0] != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(firePoint.position, targetsInRange[0].position);
        }
    }
}

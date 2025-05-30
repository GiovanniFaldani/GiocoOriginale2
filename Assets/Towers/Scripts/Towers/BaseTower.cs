using UnityEngine;
using System.Collections.Generic;  

public class BaseTower : MonoBehaviour
{
    [Header("Fire Rate Setting")]
    // Numero di colpi al secondo che la torretta può sparare
    [SerializeField] public float fireRate;

    [Header("Parameters")]
    // Tempo (in secondi) dopo il quale la torretta può sparare di nuovo
    private float nextFireTime = 0f;

    // Lista dei bersagli attualmente nel raggio d’azione
    public List<Transform> targetsInRange = new List<Transform>();

    private Projectile_Pool pool;

    private void Start()
    {
        pool = GetComponent<Projectile_Pool>();
    }

    private void Update()
    {
        // Se c'è almeno un bersaglio e siamo oltre il tempo di ricarica...
        if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
        {
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
}

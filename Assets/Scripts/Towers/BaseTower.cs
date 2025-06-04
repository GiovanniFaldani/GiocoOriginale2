using UnityEngine;
using System.Collections.Generic;  

public class BaseTower : MonoBehaviour
{
    // Punto da cui parte il proiettile
    [SerializeField] public Transform firePoint;

    [Header("Fire Rate Setting")]
    // Numero di colpi al secondo che la torretta può sparare
    [SerializeField] public float fireRate;

    [Header("Parameters")]
    // Tempo (in secondi) dopo il quale la torretta può sparare di nuovo
    private float nextFireTime = 0f;

    // Lista dei bersagli attualmente nel raggio d’azione
    public List<Transform> targetsInRange = new List<Transform>();

    private BaseProjectile_Pool pool;

    private void Start()
    {
        pool = GetComponent<BaseProjectile_Pool>();
    }

    private void Update()
    {
        CleanNullTargets();

        // Se c'è almeno un bersaglio e siamo oltre il tempo di ricarica...
        if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
        {
            Transform target = targetsInRange[0];

            if (target != null && !target.gameObject.GetComponent<Enemy>().HP.IsDead)
            {
                Debug.Log($"[BaseTower] Sparo a: {target.name}");

                // Spara al primo bersaglio nella lista
                ShootAt(targetsInRange[0]);

                // Imposta il prossimo momento in cui la torretta può sparare
                nextFireTime = Time.time + (1f / fireRate);
            }
            else
            {
                Debug.Log("[BaseTower] Il bersaglio non è valido.");
            }
        }
    }


    // Crea un proiettile e lo invia verso il bersaglio
    private void ShootAt(Transform target)
    {
        Debug.Log($"[BaseTower] Attivo proiettile contro: {target.name}");
        pool.ChooseProjectile().ActivateProjectile(target);
    }

    // Aggiunge un nemico alla lista quando entra nel trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Add(other.transform);
            Debug.Log($"[BaseTower] Nemico entrato nel raggio: {other.name}");
        }
    }

    // Rimuove il nemico dalla lista quando esce dal trigger
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);
            Debug.Log($"[BaseTower] Nemico uscito dal raggio: {other.name}");
        }
    }

    // Pulisce la lista dei bersagli
    private void CleanNullTargets()
    {
        // Creiamo una nuova lista per i bersagli validi (non null)
        List<Transform> validTargets = new List<Transform>();

        // Controlliamo uno per uno gli elementi nella lista
        foreach (Transform t in targetsInRange)
        {
            if (t != null && !t.gameObject.GetComponent<Enemy>().HP.IsDead)
            {
                validTargets.Add(t); // aggiungiamo solo quelli validi
            }
            else
            {
                Debug.Log("[BaseTower] Rimosso bersaglio nullo o morto.");
            }
        }

        // Sostituiamo la lista originale con quella pulita
        targetsInRange = validTargets;
    }
}

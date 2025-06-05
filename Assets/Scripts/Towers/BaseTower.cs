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
    protected float nextFireTime = 0f;

    // Lista dei bersagli attualmente nel raggio d’azione
    public List<Transform> targetsInRange = new List<Transform>();

    protected Projectile_Pool pool;

    public float timer;
    public float timerCooldown;

    protected void Start()
    {
        pool = GetComponent<Projectile_Pool>();
    }

    protected void Update()
    {
        CleanNullTargets();

        if (timer < 0)
        {
            SearchTarget();
        }
        else
        { 
            timer -= Time.deltaTime;
        }
    }

    protected virtual void SearchTarget()
    {
        // Se c'è almeno un bersaglio
        if (targetsInRange.Count > 0 )
        {
            Transform target = targetsInRange[0];

            if (target != null && !target.gameObject.GetComponent<Enemy>().HP.IsDead)
            {
                Debug.Log($"[BaseTower] Sparo a: {target.name}");

                // Spara al primo bersaglio nella lista
                ShootAt(target);

            }
            else
            {
                Debug.Log("[BaseTower] Il bersaglio non è valido.");
            }
        }
    }


    // Crea un proiettile e lo invia verso il bersaglio
    protected void ShootAt(Transform target)
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
        }
    }

    // Rimuove il nemico dalla lista quando esce dal trigger
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            targetsInRange.Remove(other.transform);            
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

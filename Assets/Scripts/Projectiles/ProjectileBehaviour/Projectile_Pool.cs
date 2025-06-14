using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class Projectile_Pool : MonoBehaviour
{
    [SerializeField] List<BaseProjectile> projectilePool = new List<BaseProjectile>();

    public Transform spawnPosition;

    public GameObject projectile_Prefab;

    public int poolSize;

    
    private void Start()
    {
        //instanzio gli oggetti in scena e aggiungo gli script alla lista
        for (int i = 0; i < poolSize; i++)
        {
            GameObject proj;
            proj = Instantiate(projectile_Prefab, spawnPosition.position, Quaternion.identity);
            proj.transform.parent = transform;
            projectilePool.Add(proj.GetComponent<BaseProjectile>());
        }

        //disattivo oggetti instanziati
        for (int i = 0; i < projectilePool.Count; i++)
        {
            projectilePool[i].resetPosition = spawnPosition;
            projectilePool[i].DeactivateProjectile(); 
        }
    }

    //controllo se ho script del proiettile disponibili
    public BaseProjectile ChooseProjectile()
    {
        foreach (BaseProjectile p in projectilePool)
        {
            if (!p.bIsActive) //controllo se ho proiettili non attivi in scena
            { 
                return p;  //ritorna il primo proiettile disponibile
            }
        }
        return null; //ritorna null se non ho proiettili disponibili
    }
}

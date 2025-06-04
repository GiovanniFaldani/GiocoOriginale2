using System.Collections.Generic;
using UnityEngine;

public class MageProjectile_Pool : MonoBehaviour
{
    [SerializeField] List<MageProjectile> projectilePool = new List<MageProjectile>();

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
            projectilePool.Add(proj.GetComponent<MageProjectile>());
        }
        //disattivo oggetti instanziati
        for (int i = 0; i < projectilePool.Count; i++)
        {
            projectilePool[i].resetPosition = spawnPosition;
            projectilePool[i].DeactivateProjectile();
        }
    }

    //controllo se ho script del proiettile disponibili
    public MageProjectile ChooseProjectile()
    {
        foreach (MageProjectile p in projectilePool)
        {
            if (!p.bIsActive) //controllo se ho proiettili non attivi in scena
            {
                return p;  //ritorna il primo proiettile disponibile
            }
        }
        return null; //ritorna null se non ho proiettili disponibili
    }
}

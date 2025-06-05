using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlchemistTower : BaseTower
{
    private PlacementGrid grid;

    protected void Start()
    {
        base.Start();
        grid = FindAnyObjectByType<PlacementGrid>();
    }

    protected override void SearchTarget()
    {
        // Se c'è almeno un bersaglio e siamo oltre il tempo di ricarica...
        if (targetsInRange.Count > 0 && Time.time >= nextFireTime)
        {
            Transform target = SearchNode();

            if (target != null && !target.gameObject.GetComponent<Enemy>().HP.IsDead)
            {
                Debug.Log($"[BaseTower] Sparo a: {target.name}");

                // Spara al primo bersaglio nella lista
                ShootAt(target);

                // Imposta il prossimo momento in cui la torretta può sparare
                nextFireTime = Time.time + (1f / fireRate);
            }
            else
            {
                Debug.Log("[BaseTower] Il bersaglio non è valido.");
            }
        }
    }

    //ritorna posizione del quadrato nella griglia con il numero massimo di nemici
    protected Transform SearchNode()
    {
        Dictionary<GridSquare, int> nodeCount = new Dictionary<GridSquare, int>();

        if (targetsInRange.Count > 0)
        {
            foreach (Transform t in targetsInRange)
            {
                GridSquare node = grid.GetGridSnap(t.position);

                nodeCount[node] = nodeCount.ContainsKey(node) ? nodeCount[node] + 1 : 1;
            }

            if (nodeCount.Count > 0)
            {
                var maxNode = nodeCount.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                foreach (Transform t in targetsInRange)
                {
                    if (maxNode == grid.GetGridSnap(t.position))
                    {
                        return t;
                    }
                }
            }            
        }
        return targetsInRange[0];
    }
}

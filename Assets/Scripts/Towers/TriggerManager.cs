using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public BaseTower tower;

    private void OnTriggerEnter(Collider other)
    {
        if (tower != null)
        {
            tower.OnTriggerEnter(other);
            Debug.Log($"[BaseTower] Nemico entrato nel raggio: {other.name}");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tower != null)
        {
            tower.OnTriggerExit(other);
            Debug.Log($"[BaseTower] Nemico uscito dal raggio: {other.name}");
        }
    }
}

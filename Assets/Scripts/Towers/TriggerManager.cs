using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public BaseTower tower;

    private void OnTriggerEnter(Collider other)
    {
        if (tower != null)
        {
            tower.OnTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (tower != null)
        {
            tower.OnTriggerExit(other);
        }
    }
}

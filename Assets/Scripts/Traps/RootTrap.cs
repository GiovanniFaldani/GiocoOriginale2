using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootTrap : MonoBehaviour
{
    public float debuffAmount;
    public float debuffDuration;
    private NavMeshAgent targetAgent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.speed -= debuffAmount;
                Invoke("RestoreSpeed", debuffDuration);
                Debug.Log("slowed by trap" + agent.speed);
            }
        }
    }

    private void RestoreSpeed()
    {
        if (targetAgent != null)
        { 
            targetAgent.speed += debuffAmount;
            Debug.Log("add slow debuff" + targetAgent.speed);
        }

    }
}

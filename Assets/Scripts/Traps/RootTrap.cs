using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootTrap : MonoBehaviour
{
    public float debuffAmount;
    public float debuffDuration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {  other.GetComponent<Enemy>().ApplyDebuff(debuffAmount,debuffDuration);}
    }
}

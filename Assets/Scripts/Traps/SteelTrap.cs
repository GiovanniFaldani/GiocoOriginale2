using System.Collections;
using UnityEngine;

public class SteelTrap : MonoBehaviour
{
    public float trapDamage;
    public float cooldown;
    public bool bIsOnCooldown;

    private void OnTriggerEnter(Collider other)
    {
        if (bIsOnCooldown)
        {
            return;
        }

        if (other.CompareTag("Enemy"))
        { 
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(trapDamage);
                StartCoroutine(StartCooldown());
                Debug.Log("damaged by trap" + enemy.HP.CurrentHealth);
            }
        }
    }

    public IEnumerator StartCooldown()
    { 
        bIsOnCooldown = true;
        yield return new WaitForSeconds(cooldown);
        bIsOnCooldown = false;
    }
}

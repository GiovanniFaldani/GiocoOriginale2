using System.Collections;
using UnityEngine;

public class DamageOvertime : MonoBehaviour
{
    public float DotDamage;
    public float interval;
    public float duration;

    private Enemy enemy;
    private Coroutine DamageCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponent<Enemy>(); //salvo componente script enemy
    }

    //metodo per attivazione coroutine
    public void StartEffect()
    {
        if (DamageCoroutine != null) //controllo se a damageCoroutine viene assegnata la coroutine 
        {
            StopCoroutine(DamageCoroutine);
        }
        DamageCoroutine = StartCoroutine(ApplyDamageOvertime()); //avvio effetti coroutine
    }

    private IEnumerator ApplyDamageOvertime()
    {
        float timeElapsed = 0; //tempo trascorso
        while (timeElapsed < duration)
        {
            if (enemy != null && !enemy.HP.IsDead) //se presente componente enemy && enemy non è morto
            {
                enemy.TakeDamage(DotDamage); //applica danno
            }
            yield return new WaitForSeconds(interval); //attesa x secondi 
            timeElapsed += interval; //reset tempo trascorso per ripetere effetto
        }
    }
}

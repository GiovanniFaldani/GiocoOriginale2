using UnityEngine;

public class FortressHP : MonoBehaviour
{
    public float fortressHP;
    public float fortressMaxHP;
    public bool bIsFortressDead;

    private void Awake()
    {
        fortressHP = fortressMaxHP;
        bIsFortressDead = false;
    }

    public void FortressTakeDamage(float damage)
    {
        fortressHP = fortressHP - damage;
    }

    public void FortressDeath()
    {
        if (fortressMaxHP <= 0)
        {
            bIsFortressDead = true;
            GameManager.Instance.GameOver();
        }
    }
}

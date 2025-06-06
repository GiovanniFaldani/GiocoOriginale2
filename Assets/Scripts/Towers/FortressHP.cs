using UnityEngine;

public class FortressHP : MonoBehaviour
{
    public int fortressHP;
    public int fortressMaxHP;
    public bool bIsFortressDead;

    private void Awake()
    {
        fortressHP = fortressMaxHP;
        bIsFortressDead = false;
    }

    public void FortressTakeDamage(int damage)
    {
        fortressHP = fortressHP - damage;
        GameManager.Instance.AddToHp(-damage);
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

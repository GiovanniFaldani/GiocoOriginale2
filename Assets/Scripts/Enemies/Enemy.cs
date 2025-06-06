using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

[Serializable]
public struct Health
{
    public float MaxHealth;
    public float CurrentHealth;
    public bool IsDead;
}
public class Enemy : MonoBehaviour
{
    [SerializeField] bool testing;

    public GridSquare currentSquare;
    private Pathfinding PF;

    private Transform meshPivot;
    private float deathTimer = 0; 

    [Header("Enemy variables")]
    public Health HP;
    [SerializeField] private float speed;
    [SerializeField] private float moveTimer = 0;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackTimer = 0;
    [SerializeField] public float waveWeight;
    [Space(10)]

    [Header("Test Animation variables")]
    [SerializeField] float fallOverTime = 1;
    [SerializeField] float disappearTime = 1;
    [SerializeField] float gridSpeed = 0.25f;

    private void Awake()
    {
        meshPivot = GetComponentInChildren<Transform>();
        PF = GetComponent<Pathfinding>();
    }
    void Start()
    {
        HP.CurrentHealth = HP.MaxHealth;
        HP.IsDead = false;
        moveTimer = speed;
        attackTimer = attackSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (meshPivot.localPosition.y > 0)
        { meshPivot.localPosition += Vector3.down * Time.deltaTime; }

        if (!HP.IsDead)
        {
            if (currentSquare.fortress)
            {
                if (attackTimer <= 0)
                { 
                    GameManager.Instance.AddToHp(-attackDamage);
                    meshPivot.localPosition += Vector3.up;
                    attackTimer = attackSpeed;
                }
            }
            else
            {
                Debug.DrawLine(transform.position,currentSquare.worldPosition + Vector3.down,Color.red);
                if (Vector3.Distance(transform.position, currentSquare.worldPosition + Vector3.down) < 0.2f)
                {
                    moveTimer -= Time.deltaTime;
                    if (moveTimer <= 0)
                    {
                        currentSquare = PF.FindPath(transform.position, Vector3.up)[0]; 
                        moveTimer = speed;
                    }
                }
                else
                { 
                    transform.Translate((currentSquare.worldPosition + Vector3.down - transform.position).normalized * Time.deltaTime * gridSpeed,Space.World);
                    transform.rotation = Quaternion.LookRotation((currentSquare.worldPosition + Vector3.down - transform.position).normalized);
                }
            }
        }
        else
        {
            deathTimer += Time.deltaTime;
            if (deathTimer <= fallOverTime)
            { meshPivot.localRotation = Quaternion.Euler(Vector3.Lerp(Vector3.zero, new Vector3(90, 0, 0), deathTimer / fallOverTime)); }
            else
            { meshPivot.localRotation = Quaternion.Euler(new Vector3(90, 0, 0)); }

            if (deathTimer >= fallOverTime + disappearTime)
            { Destroy(gameObject); }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!HP.IsDead)
        {
            HP.CurrentHealth = Mathf.Clamp(HP.CurrentHealth - damage, 0, HP.MaxHealth);
            if (HP.CurrentHealth <= 0)
            { Death(); }
        }
    }

    protected virtual void Death()
    {
        HP.IsDead = true;
        GetComponent<Collider>().enabled = false;
        WaveHandler.Instance.enemiesAlive--;
    }
}

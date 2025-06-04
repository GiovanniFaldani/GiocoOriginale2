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

    private GridSquare currentSquare;
    private Pathfinding PF;

    private NavMeshAgent agent;
    private Transform meshPivot;
    private float deathTimer = 0; 

    [Header("Enemy variables")]
    public Health HP;
    [SerializeField] private float speed;
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float waveWeight;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!HP.IsDead)
        {
            TestInputs();
            
            if(agent.remainingDistance < 0.2 && !testing)
            { 
                Debug.Log("Enemy " + gameObject.name + " damaged the fort for " + attackDamage + " damage!"); 
                //GameManager.Instance.DamageFort(fortDamage);

                //Temporary
                Destroy(gameObject);
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

    public void TestInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            { agent.SetDestination(hit.point); }
        }

        if (Input.GetKeyDown(KeyCode.Space)) { TakeDamage(1); }
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
        agent.speed = 0;
        GetComponent<Collider>().enabled = false;
    }
    private void MoveCheck()
    {
        List<GridSquare> squareCheck = PF.FindPath(currentSquare.worldPosition, Vector3.zero);
        foreach (GridSquare square in squareCheck)
        {
            Debug.Log(square.gridX + " - " + square.gridY);
        }
    }
    private void ArriveCheck()
    {

    }
}

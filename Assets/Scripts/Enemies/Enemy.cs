using System;
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

    public Health HP;
    [SerializeField] private float speed;
    [SerializeField] private float fortDamage;

    private NavMeshAgent agent;
    [SerializeField] Transform meshPivot;

    [SerializeField] float fallOverTime = 1;
    [SerializeField] float disappearTime = 1;
    private float deathTimer = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        meshPivot = GetComponentInChildren<Transform>();
    }
    void Start()
    {
        HP.CurrentHealth = HP.MaxHealth;
        HP.IsDead = false;
    }
    private void OnValidate()
    {
        if (agent != null)
        { agent.speed = speed; }
    }

    // Update is called once per frame
    void Update()
    {
        if (!HP.IsDead)
        {
            TestInputs();
            
            if(agent.remainingDistance < 0.2)
            { 
                Debug.Log("Enemy " + gameObject.name + " damaged the fort for " + fortDamage + " damage!"); 
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
            else if (deathTimer >= fallOverTime + disappearTime)
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
            {
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        HP.IsDead = true;
        GetComponent<Collider>().enabled = false;
    }
}

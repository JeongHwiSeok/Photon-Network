using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    MOVE,
    ATTACK,
    DIE
}

public class Metalon : MonoBehaviour
{
    private int health;
    public int Health
    {
        set { health = value; }
        get { return health; }
    }

    [SerializeField] State state;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    [SerializeField] Transform turretPosition;

    private void Start()
    {
        turretPosition = GameObject.Find("Turret Tower").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        health = 100;
    }

    private void Update()
    {
        switch (state)
        {
            case State.MOVE : Move();
                break;
            case State.ATTACK : Attack();
                break;
            case State.DIE : Die();
                break;
        }
    }

    public void Move()
    {
        navMeshAgent.SetDestination(turretPosition.position);
    }

    public void Attack()
    {
        animator.SetBool("Attack", true);
    }

    public void Die()
    {
        animator.Play("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Turret Tower"))
        {
            state = State.ATTACK;
        }
    }
}

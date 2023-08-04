using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemySO data;
    public Rigidbody2D rb;
    public Animator anim;
    [HideInInspector] public Transform attackTarget;

    public bool chase;

    public int currentHP;
    public GameObject deathEffect;

    public string STATE;
    public Enemy_Abstract currentState;
    public Enemy_PatrolState patrolState = new Enemy_PatrolState();
    public Enemy_IdleState idleState = new Enemy_IdleState();
    public Enemy_AttackState attackState = new Enemy_AttackState();

    public Transform[] patrolPoints;
    [HideInInspector] public Vector2 startPos;
    [HideInInspector] public Quaternion startRot;

    void Start()
    {
        if (data.flyer)
        {
            currentState = idleState;
            startPos = transform.position;
            startRot = transform.rotation;
        }
        else
        {
            currentState = patrolState;
        }
        currentState.EnterState(this);
        currentHP = data.maxHP;
    }

    void Update()
    {
        attackTarget = PlayerController.instance.transform;

        if (Vector3.Distance(transform.position, attackTarget.position) < data.attackRange)
        {
            chase = true;
        }
        else
        {
            chase = false;
        }

        currentState.UpdateLogics(this);
        STATE = currentState.ToString();
    }
    void FixedUpdate()
    {

        currentState.UpdatePhysics(this);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamageToPlayer(data.damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DealDamageToPlayer(data.damage);
        }
    }

    public void SwitchState(Enemy_Abstract newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            AudioController.instance.PlaySfxAdjusted(11);
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public enum EnemyState
{
    PATROL,
    PERSUE,
    ATTACK,
    FLEE
}


public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRender;

    [Header("Movement Settings")]
    public EnemyAnimState enemyAnimState;
    public bool isFacingRight = true;
    
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public Transform lookAhead;
    public bool hasGroundAhead;


    [Header("Enemy Abilities")]
    public HealthBarController healthBar;
    public Transform healthBarTransform;
    public float maxHealth = 10.0f;
    public float currentHealth = 10.0f;
    public float strength = 10.0f;
    public float toughness = 1.0f;
    public float speed = 1.5f;

    [Header("AI Settings")]
    public Transform lineOfSight;
    public EnemyState enemyState = EnemyState.PATROL;
    public bool playerHasBeenSeen;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnimState = EnemyAnimState.WALK;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckLineOfSight();
        CheckEnemyState();
        TakeAction();

        if (Input.GetKeyDown(KeyCode.K))
        {
            currentHealth -= 1.0f;
            healthBar.SetDamage(0.1f);
        }
    }

    private void TakeAction()
    {
        switch (enemyState)
        {
            case EnemyState.PATROL:
                speed = 1.5f;
                break;
            case EnemyState.PERSUE:
                speed = 5.0f;

                break;
            case EnemyState.ATTACK:
                // jump at the player

                break;
            case EnemyState.FLEE:

                // run away from the player's position
                break;
        }
    }

    private void CheckEnemyState()
    {
        if (playerHasBeenSeen)
        {
            if (currentHealth > (0.5 * maxHealth))
            {
                enemyState = EnemyState.PERSUE;
            }

            if (Vector2.Distance(transform.position, player.position) <= 1.0f)
            {
                enemyState = EnemyState.ATTACK;
            }

            if (Vector2.Distance(transform.position, player.position) >= 10.0f)
            {
                enemyState = EnemyState.PATROL;
            }
        }

        if (currentHealth < (0.3 * maxHealth))
        {
            enemyState = EnemyState.FLEE;
        }

    }

    void Move()
    {
        isGrounded = Physics2D.BoxCast(
            transform.position,
            new Vector2(2.0f, 1.0f), 0.0f,
            Vector2.down, 1.0f,
            1 << LayerMask.NameToLayer("Ground"));

        hasGroundAhead = Physics2D.Linecast(
            transform.position,
            lookAhead.position,
            1 << LayerMask.NameToLayer("Ground"));

        if (isGrounded && !hasGroundAhead)
        {
            //spriteRender.flipX = (hasGroundAhead) ? false : true;

            transform.localScale = new Vector3(-transform.localScale.x, 1.0f, 1.0f);
            isFacingRight = !isFacingRight;
        }
        

        if (isFacingRight && isGrounded)
        {
            enemyRigidBody.velocity = new Vector2(speed, 0.0f);
        }

        if (!isFacingRight && isGrounded)
        {
            enemyRigidBody.velocity = new Vector2(-speed, 0.0f);
        }

        healthBar.gameObject.transform.position = healthBarTransform.position - new Vector3(0.6f, 0.0f, 0.0f);
    }

    void CheckLineOfSight()
    {
        if (!playerHasBeenSeen)
        {
            playerHasBeenSeen = Physics2D.Linecast(
                transform.position,
                lineOfSight.position,
                1 << LayerMask.NameToLayer("Player"));

            if (playerHasBeenSeen)
            {
                var hit = Physics2D.Linecast(
                    transform.position,
                    lineOfSight.position,
                    1 << LayerMask.NameToLayer("Player"));

                player = hit.transform;
            }
        }
        

        Debug.DrawRay(this.transform.position, new Vector2(Mathf.Clamp(lineOfSight.position.x, 0.0f, 1.0f) * (lineOfSight.position.x - transform.position.x), 0.0f), Color.yellow);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            currentHealth -= 1.0f;
            healthBar.SetDamage(0.1f);
        }
    }
}

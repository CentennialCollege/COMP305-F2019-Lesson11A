using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class EnemyController : MonoBehaviour
{
    public Animator animator;
    public SpriteRenderer spriteRender;

    public EnemyAnimState enemyAnimState;
    public bool isFacingRight = true;
    public float speed = 5.0f;
    public Rigidbody2D enemyRigidBody;
    public bool isGrounded;
    public Transform lookAhead;
    public bool hasGroundAhead;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyAnimState = EnemyAnimState.WALK;
    }

    // Update is called once per frame
    void Update()
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

        Move();
    }

    void Move()
    {
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

    }
}

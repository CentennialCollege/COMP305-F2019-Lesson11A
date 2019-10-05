using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HeroController : MonoBehaviour
{
    public Animator animator;

    public HeroAnimState heroAnimState;

    public SpriteRenderer spriteRenderer;

    public Transform groundTarget;

    public Rigidbody2D playerRigidBody;


    public bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;
        grounded = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector2.down *2, Color.yellow);

        bool hit = Physics2D.Linecast(
                transform.position,
                groundTarget.position,
                1 << LayerMask.NameToLayer("Ground"));


        grounded = hit;




        // Idle
        if(Input.GetAxis("Horizontal") == 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }


        // moving to the right
        if((Input.GetAxis("Horizontal") > 0) && (grounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = false;
            playerRigidBody.AddForce(Vector2.right * 30.0f);
        }

        // moving to the left
        if((Input.GetAxis("Horizontal") < 0) && (grounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = true;
            playerRigidBody.AddForce(Vector2.left * 30.0f);
        }

        // jumping
        if((Input.GetAxis("Jump") > 0) && (grounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
            playerRigidBody.AddForce(Vector2.up * 900.0f);
            grounded = false;
        }

        // not jumping
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }
    }
}

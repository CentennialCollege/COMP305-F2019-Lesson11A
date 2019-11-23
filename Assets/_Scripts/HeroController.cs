using System;
using System.Collections;
using System.Collections.Generic;
//using System.Runtime.Remoting.Channels;
using UnityEngine;
using Util;

public class HeroController : MonoBehaviour
{
    public Animator animator;
    public HeroAnimState heroAnimState;
    public SpriteRenderer spriteRenderer;
    public Transform groundTarget;
    public Rigidbody2D playerRigidBody;
    public bool isGrounded;
    public AudioSource jumpSound;
    public float jumpForce = 200.0f;
    public Vector2 maximumVelocity = new Vector2(6.0f, 12.0f);

    public LayerMask layermask;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;
        isGrounded = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector2.down *2, Color.yellow);

        transform.position = new Vector3(
            transform.position.x, 
            transform.position.y, 
            0.0f);

        //bool hit = Physics2D.Linecast(
        //        transform.position,
        //        groundTarget.position,
        //        1 << LayerMask.NameToLayer("Ground"));


        isGrounded = Physics2D.BoxCast(
            transform.position, 
            new Vector2(2.0f, 1.0f), 0.0f, 
            Vector2.down, 1.0f, 
            layermask);


        // Idle
        if(Input.GetAxis("Horizontal") == 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }


        // moving to the right
        if((Input.GetAxis("Horizontal") > 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = false;
            playerRigidBody.AddForce(Vector2.right * 30.0f);
        }

        // moving to the left
        if((Input.GetAxis("Horizontal") < 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = true;
            playerRigidBody.AddForce(Vector2.left * 30.0f);
        }

        // jumping
        if((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
            jumpSound.Play();
            playerRigidBody.AddForce(Vector2.up * jumpForce);
           
            isGrounded = false;
        }

        // not jumping
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }

        playerRigidBody.velocity =
            new Vector2(
                Mathf.Clamp(playerRigidBody.velocity.x,
                    -maximumVelocity.x, maximumVelocity.x),
                Mathf.Clamp(playerRigidBody.velocity.y,
                    -maximumVelocity.y, maximumVelocity.y));
    }
}

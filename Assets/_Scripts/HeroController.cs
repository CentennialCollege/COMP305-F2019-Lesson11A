using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class HeroController : MonoBehaviour
{
    public Animator animator;

    public HeroAnimState heroAnimState;

    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        heroAnimState = HeroAnimState.IDLE;

        
    }

    // Update is called once per frame
    void Update()
    {
        // Idle
        if(Input.GetAxis("Horizontal") == 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }


        // moving to the right
        if(Input.GetAxis("Horizontal") > 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = false;
        }

        // moving to the left
        if(Input.GetAxis("Horizontal") < 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.WALK);
            heroAnimState = HeroAnimState.WALK;
            spriteRenderer.flipX = true;
        }

        // jumping
        if(Input.GetAxis("Jump") > 0)
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.JUMP);
            heroAnimState = HeroAnimState.JUMP;
        }

        // not jumping
        if(Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetInteger("AnimState", (int)HeroAnimState.IDLE);
            heroAnimState = HeroAnimState.IDLE;
        }
    }
}

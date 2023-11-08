using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charcator_animation : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    public enum PLAYERSTATE 
    {
        standing,
        walking,
        jumping,
        dead
    }
    
    public PLAYERSTATE state = PLAYERSTATE.standing;
    

    void Start()
    {
        // mario initial state
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];

        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("levelup", false);
        //mario state update
        if (Input.GetKeyDown("p"))
        {
            // big mario
            spriteRenderer.sprite = sprites[1];
            animator.SetBool("levelup", true);

        }
        else if (Input.GetKeyDown("g"))
        {
            // small mario
            spriteRenderer.sprite = sprites[0];
        }

        if (state == PLAYERSTATE.dead)
        {
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("dead", true);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);


        }

        if (GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            state = PLAYERSTATE.jumping;
            animator.SetBool("jumping", true);
        }
        else
        {
            state = PLAYERSTATE.walking;
            animator.SetBool("jumping", false);
        }

        if (GetComponent<Rigidbody2D>().velocity.x != 0 && state != PLAYERSTATE.jumping)
        {

            state = PLAYERSTATE.walking;
            animator.SetBool("walking", true);

        }
        else
        {
            state = PLAYERSTATE.standing;
            animator.SetBool("walking", false);
        }

    }
}

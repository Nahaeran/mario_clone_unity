using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Charcator_animation : MonoBehaviour
{
    
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
        Animator animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Animator animator = GetComponent<Animator>();
        if (state == PLAYERSTATE.dead)
        {
            GetComponent<Collider2D>().enabled = false;
            //GetComponent<PlayerScript>().Jump();
            animator.SetBool("dead", true);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 500);
            

        }
            
        if (GetComponent<Rigidbody2D>().velocity.y != 0)
        {
            state = PLAYERSTATE.jumping;
            animator.SetBool("jumping", true);
        } else
        {
            state = PLAYERSTATE.walking;
            animator.SetBool("jumping", false);
        }

        if (GetComponent<Rigidbody2D>().velocity.x != 0 && state != PLAYERSTATE.jumping) 
        {
            
            state = PLAYERSTATE.walking;
            animator.SetBool("walking" , true);

        } else
        {
            state = PLAYERSTATE.standing;
            animator.SetBool("walking", false);
        }

        


    }
}

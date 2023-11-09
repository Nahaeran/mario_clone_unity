using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_kill_collider : MonoBehaviour
{
    public GameObject GameObject;
    private void OnTriggerEnter2D(Collider2D collision)
    // 2D 충돌 객체를 반환한다 이 OnTriggerEnter2D는 또다른 
    {
        if (GameObject.tag == "Enemy")
        {
            if (collision.tag == "Player" && GetComponent<AutomateMovement>().state != AutomateMovement.Enemystate.dead)
            {
                collision.GetComponent<Charcator_animation>().state = Charcator_animation.PLAYERSTATE.dead;

            }
        } else if (GameObject.tag == "hole")
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<Charcator_animation>().state = Charcator_animation.PLAYERSTATE.dead;
                collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 800);
            }
        }
        
    }
}   

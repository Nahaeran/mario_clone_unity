using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int level;
    public int playerSpeed = 10;
    public int playerJumpPower = 800;
    
    public float moveX;
    // Update is called once per frame
    public bool is_jumping = true;

    void Update()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        // 버튼 이벤트
        moveX = Input.GetAxis("Horizontal");
        if (moveX > 0f){
            transform.eulerAngles = Vector3.zero;
        }
        else if (moveX < 0f) {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (is_jumping == false){
                is_jumping = true;
                Jump();
            }
        } // playerSpeed 만큼 이동
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
    }
    void OnCollisionEnter2D(Collision2D col) {    
        if (col.transform.tag == "floor" || col.transform.tag == "pipe"){        
            is_jumping = false;    
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}

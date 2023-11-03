using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shell;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y)
        // {   
        //     Debug.Log(collision.transform.position.y);
        //     Debug.Log(transform.position.y);
        //     Instantiate(shell, gameObject.transform.position, gameObject.transform.rotation);
        //     Destroy(gameObject);
        // }
        if (collision.gameObject.CompareTag("Player")){
            Instantiate(shell, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
        }
    }

}

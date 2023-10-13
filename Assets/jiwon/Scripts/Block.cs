using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int block_health;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y < transform.position.y
        && collision.gameObject.GetComponent<PlayerMoveCopy>().level > 1)
        {
            block_health -= 1;
        }

        if (block_health <= 0){
            Destroy(gameObject);
        }

    }


}

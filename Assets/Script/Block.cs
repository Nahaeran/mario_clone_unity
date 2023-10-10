using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block : MonoBehaviour
{
    // Start is called before the first frame update
    public int block_health = 1;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y < transform.position.y
        && collision.gameObject.GetComponent<PlayerScript>().level > 1)
        {
            block_health -= 1;
            Debug.Log(collision.gameObject.GetComponent<PlayerScript>());
        }

        if (block_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

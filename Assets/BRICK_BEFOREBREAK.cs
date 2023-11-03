using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRICK_BEFOREBREAK : MonoBehaviour
{

    
    public int block_health;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject block = transform.Find("Brick").gameObject;
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y < transform.position.y
        && collision.gameObject.GetComponent<PlayerScript>().level > 1)
        {
            Debug.Log('g');
            Debug.Log(block);
            block_health -= 1;
        }

        if (block_health <= 0)
        {
            //gameObject.SetActive(false);
            //gameObject.GetComponent<scrip>().state = 1;
            GameObject particle1 = transform.Find("block1").gameObject;
            GameObject particle2 = transform.Find("block2").gameObject;
            GameObject particle3 = transform.Find("block3").gameObject;
            GameObject particle4 = transform.Find("block4").gameObject;
            particle1.GetComponent<scrip>().state = 1;
            particle2.GetComponent<scrip>().state = 1;
            particle3.GetComponent<scrip>().state = 1;
            particle4.GetComponent<scrip>().state = 1;
            Destroy(block);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject, 2);
        }

    }
}

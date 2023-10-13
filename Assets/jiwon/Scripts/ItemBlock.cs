using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBlock : MonoBehaviour
{
    public int block_health;
    public bool breakable;
    public GameObject item;
    public GameObject empty;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y < transform.position.y)
        {
            block_health -= 1;
            Debug.Log(collision.gameObject.GetComponent<PlayerMoveCopy>());
            if (item != null){
                Instantiate(item, gameObject.transform.position, gameObject.transform.rotation);
                SpriteRenderer render = GetComponent<SpriteRenderer>();
                render.enabled = true;
            }
        }

        if (block_health <= 0){
            if (! breakable){
                Instantiate(empty, gameObject.transform.position, gameObject.transform.rotation);
            }
            Destroy(gameObject);
        }

    }


}
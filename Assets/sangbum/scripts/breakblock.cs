using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class scrip : MonoBehaviour
{
    public Vector2 direction;
    public int state;
    private float velocity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {   
        if (state == 1)
        {
            Vector3 pos = transform.localPosition;
            pos.x += direction.x * velocity * Time.deltaTime;
            pos.y += direction.y * velocity * Time.deltaTime;
            transform.localPosition = pos;
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 3;
            Destroy(gameObject, 1);
        }

    }
}

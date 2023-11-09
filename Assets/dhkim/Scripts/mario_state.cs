using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioState : MonoBehaviour
{
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0]; 
    }

    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if (Input.GetKeyDown("g"))
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}
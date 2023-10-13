using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate());
    }

    // Update is called once per frame
    private IEnumerator Animate()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        BoxCollider2D trigger = GetComponents<BoxCollider2D>()[0];
        BoxCollider2D collider = GetComponents<BoxCollider2D>()[1];
        SpriteRenderer render = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
        trigger.enabled = false;
        collider.enabled = false;
        render.enabled = false;

        yield return new WaitForSeconds(0.25f);

        render.enabled = true;
        trigger.enabled = true;

        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = transform.position + Vector3.up * 0.16f;


        while (elapsed < duration){
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        rigidbody.isKinematic = false;
        collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            Debug.Log('d');
            Destroy(gameObject);
        }
    }
}


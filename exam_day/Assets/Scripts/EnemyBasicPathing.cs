using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicPathing : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer renderer;
    public float speed = 5.0f;
    Coroutine moveCoroutine;
    private Vector2 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        moveCoroutine = StartCoroutine(Move(body, speed));
    }

    public IEnumerator Move(Rigidbody2D body, float speed)
    {
        while (true)
        {
            if(GameObject.FindWithTag("Player") == null) yield return null;
            if(GameObject.FindWithTag("Player") == null) yield break;
            playerPosition = GameObject.FindWithTag("Player").transform.position;
            Vector2 direction = playerPosition - body.position;
            direction.Normalize();
            body.velocity = direction * speed;

            //If player changes direction, flip sprite in sprite renderer component
            if (playerPosition.x > body.position.x)
            {
                renderer.flipX = false;
            }
            else if (playerPosition.x < body.position.x)
            {
                renderer.flipX = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicPathing : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer renderer;
    public float speed = 5.0f;

    public float rotationSpeed = 180.0f;
    Coroutine moveCoroutine;

    public Vector2 directionVector;
    public float directionAngle;
    private Vector2 playerPosition;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Move(Rigidbody2D body, float speed)
    {
        while (true)
        {
            
            if(GameObject.FindWithTag("Player") == null) yield return null;
            if(GameObject.FindWithTag("Player") == null) yield break;

            Player player= GameObject.FindWithTag("Player").GetComponent<Player>();
            playerPosition = player.currentPos;

            Vector2 direction = playerPosition - body.position;
            direction.Normalize();

            directionVector = direction;
            directionAngle =  Mathf.Atan2(direction[1], direction[0]) * Mathf.Rad2Deg;

            if (direction == Vector2.zero)
            {
                yield return null;
            }

            // RayCast to check if there is a wall in the way, up, down and left
            if(Physics2D.Raycast(body.position, direction, 5.0f, LayerMask.GetMask("Wall"))){
                List<Vector2> possibleDirections = new List<Vector2>();
                //Check if there is a wall to the left
                if(!Physics2D.Raycast(body.position, Vector2.left, 1.0f, LayerMask.GetMask("Wall"))){
                    possibleDirections.Add(Vector2.left); 
                }
                //Check if there is a wall to the right
                if(!Physics2D.Raycast(body.position, Vector2.right, 1.0f, LayerMask.GetMask("Wall"))){
                    possibleDirections.Add(Vector2.right); 
                }

                if(!Physics2D.Raycast(body.position, Vector2.up, 1.0f, LayerMask.GetMask("Wall"))){
                    possibleDirections.Add(Vector2.right); 
                }

                if(!Physics2D.Raycast(body.position, Vector2.down, 1.0f, LayerMask.GetMask("Wall"))){
                    possibleDirections.Add(Vector2.right); 
                }

                // calculate the angle between direction and possibleDirections
                float minAngle = 360.0f;
                Vector2 minAngleDirection = Vector2.zero;
                foreach(Vector2 possibleDirection in possibleDirections){
                    float angle = Vector2.Angle(direction, possibleDirection);
                    if(angle < minAngle){
                        minAngle = angle;
                        minAngleDirection = possibleDirection;
                    }
                }

                direction = minAngleDirection;
    
            }


            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, direction);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            body.SetRotation(rotation);

            if (direction == Vector2.zero)
            {
                body.velocity = Vector2.zero;
            }
            else
            {
                body.velocity = transform.up * speed;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void StopMoving()
    {
        StopCoroutine(moveCoroutine);
    }

    public void StartMoving()
    {
        moveCoroutine = StartCoroutine(Move(body, speed));
    }

    public void ResetPathing(){
        if(moveCoroutine != null){
            StopMoving();
        }
        StartMoving();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        
    }
}

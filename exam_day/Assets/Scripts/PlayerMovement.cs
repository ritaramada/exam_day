using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;
    SpriteRenderer renderer;
    Animator animator;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;

    private Vector2 lastDirection = Vector2.down;

    public float directionAngle = 0.0f;
    public Vector2 directionVector = Vector2.down;
    string animationState = "AnimationState";

    public float runSpeed = 10.0f;

    private float rotationSpeed = 13000.0f;

    enum PlayerState
    {
        Idle = 0,
        Walking = 1
    }

    void Start ()
    {
        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    
    }

    private void MoveCharacter(){
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down

        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        } 

        if(horizontal != 0 || vertical != 0){
            lastDirection = new Vector2(horizontal, vertical);
        }


        Vector2 direction = new Vector2(horizontal, vertical);
        Quaternion targetRotation;
        if(horizontal == 0 && vertical == 0){
            targetRotation = Quaternion.LookRotation(transform.forward, lastDirection);
        }else{
            targetRotation = Quaternion.LookRotation(transform.forward, direction);
        }
        
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        body.SetRotation(rotation);

        if (direction == Vector2.zero)
        {
            body.velocity = Vector2.zero;
        }
        else
        {
            body.velocity = transform.up * runSpeed;
        }


    }

    private void UpdateState(){

        if (horizontal != 0 || vertical != 0){
            animator.SetInteger(animationState, (int)PlayerState.Walking);

            // Calculate the direction angle in degrees of the direction vector and (1,0)
            directionAngle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            directionVector = new Vector2(horizontal, vertical);

        } else {
            animator.SetInteger(animationState, (int)PlayerState.Idle);
        }


    }

}


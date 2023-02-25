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

    string animationState = "AnimationState";

    public float runSpeed = 10.0f;

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

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        //If player changes direction, flip sprite in sprite renderer component
        if (horizontal > 0)
        {
            renderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            renderer.flipX = true;
        }

    }

    private void UpdateState(){

        if (horizontal != 0 || vertical != 0){
            animator.SetInteger(animationState, (int)PlayerState.Walking);
        } else {
            animator.SetInteger(animationState, (int)PlayerState.Idle);
        }
    }

}


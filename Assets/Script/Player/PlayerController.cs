﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerPositionState { left, right, center}


public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidBody;
    private Vector3 direction;

    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField]private float forwardMovementSpeed=1f;
    [SerializeField] private float jumpforce=10f;

    private float gravity = -20;


    private  Vector3 InitialPosition;
    private  Vector3 finalPosition;
    private float dragValue;

    private playerPositionState playerCurrentState;
    private bool isGrounded;

    private void Start()
    {
        playerRigidBody = this.GetComponent<Rigidbody>();
        transform.position = centerPosition.position;
        playerCurrentState = playerPositionState.center;

        dragValue = Screen.width / 20;

        direction = new Vector3(0,0,forwardMovementSpeed);
    }

    private void FixedUpdate()
    {
        playerRigidBody.transform.Translate(direction);
    }

    void Update()
    {
        checkForSwipeInput();
        checkIfGrounded();
    }

    private void checkIfGrounded()
    {
        //TODO: use a better system for groun check
        //cheap but will work for now
        isGrounded = (transform.position.y <= 0f) ? true : false;
    }

    private void checkForSwipeInput()
    {
        //TODO:create using Touch
        //for now doing using mouse will later change to touch

        if (Input.GetMouseButtonDown(0))
        {
            InitialPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            finalPosition = Input.mousePosition;

            //if player swipped horizontally
            if (Mathf.Abs(finalPosition.x - InitialPosition.x) > Mathf.Abs(finalPosition.y - InitialPosition.y))
            {
                if (Mathf.Abs(finalPosition.x - InitialPosition.x) > dragValue)
                {
                    if (finalPosition.x > InitialPosition.x)
                    {
                        MoveRight();
                    }
                    else
                    {
                        MoveLeft();
                    }
                }
            }
            else //verticall movement
            {
                if (finalPosition.y > InitialPosition.y)
                {
                    jump();
                    Debug.Log("jump");
                }
                else
                {

                    Debug.Log("slide");
                }

            }
        }
    }

    private void MoveRight()
    {
        if (playerCurrentState != playerPositionState.right)
        {
            //dont need to check in this as even the position transforms are changing position
            transform.position = rightPosition.position;
            //if currently in center move to right else center
            playerCurrentState = (playerCurrentState == playerPositionState.center) ? playerPositionState.right : playerPositionState.center;
        }
    }

    private void MoveLeft()
    {
        if (playerCurrentState != playerPositionState.left)
        {
            
            //dont need to check in this as even the position transforms are changing position
            transform.position = leftPosition.position;
            //if currently in center move to left else center
            playerCurrentState = (playerCurrentState == playerPositionState.center) ? playerPositionState.left : playerPositionState.center;
            
        }
    }

    private void jump()
    {
        if(isGrounded)
        {
            isGrounded = false;
            playerRigidBody.AddForce(Vector3.up * jumpforce , ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody characterRB;
    private float horizontal;

    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform centerPosition;
    [SerializeField] private Transform rightPosition;

    private playerPositionState currentPosition;

    private enum playerPositionState { left,right,center}

    private void Start()
    {
        characterRB = this.GetComponent<Rigidbody>();
        transform.position = centerPosition.position;
        currentPosition = playerPositionState.center;
    }

    private void FixedUpdate()
    {
        characterRB.transform.Translate(horizontal, 0, 1);
    }

    void Update()
    {
        if(currentPosition != playerPositionState.left)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = leftPosition.position;

                if (currentPosition == playerPositionState.center)
                {
                    currentPosition = playerPositionState.left;
                }
                else
                {
                    currentPosition = playerPositionState.center;
                }
            }
        }

        if (currentPosition != playerPositionState.right)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = rightPosition.position;

                if (currentPosition == playerPositionState.center)
                {
                    currentPosition = playerPositionState.right;
                }
                else
                {
                    currentPosition = playerPositionState.center;
                }
            }
        }
        
    }
}

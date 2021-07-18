using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public enum playerPositionState { left, right, center}


public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRigidBody;
    private PlayerHealth playerHealth;
    private Vector3 direction;

    private static float coinAmount = 0;

    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private TextMeshProUGUI coinText;

    [SerializeField] private float leftPosition;
    [SerializeField] private float centerPosition;
    [SerializeField] private float rightPosition;

    [SerializeField]private float forwardMovementSpeed=1f;
    [SerializeField] private float jumpforce=10f;

    [SerializeField] private GameManager gameManager;

    private  Vector3 InitialPosition;
    private  Vector3 finalPosition;
    private float dragValue;

    private playerPositionState playerCurrentState;
    private bool isGrounded;
    [SerializeField] private LayerMask GroundLayer;

    private Vector3 ScaleDownFactor = new Vector3(0,0.4f,0);
    private Coroutine slideCoroutine = null;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();

        //initial position set
        transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
        playerCurrentState = playerPositionState.center;

        //related to screen swipe
        dragValue = Screen.width / 20;

        //default movement direction
        direction = new Vector3(0,0,forwardMovementSpeed);

        //increase speed with time
        StartCoroutine(IncreaseSpeed());
    }

    private void FixedUpdate()
    {
        playerRigidBody.transform.Translate(direction);
        SetPosition();
    }

    private void SetPosition()
    {
        switch (playerCurrentState)
        {
            case playerPositionState.center:
            {
                transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
                break;
            }
            case playerPositionState.left:
            {
                transform.position = new Vector3(leftPosition, transform.position.y, transform.position.z);
                break;
            }
            case playerPositionState.right:
            {
                transform.position = new Vector3(rightPosition, transform.position.y, transform.position.z);
                break;
            }
        }
    }


    void Update()
    {
        checkForSwipeInput();
        checkIfGrounded();
    }

    private void LateUpdate()
    {
        updateScore();
    }

    IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (forwardMovementSpeed < 1.5)
            {
                forwardMovementSpeed += 0.025f;
                direction = new Vector3(0, 0, forwardMovementSpeed);
            }
            else
            {
                StopCoroutine(IncreaseSpeed());
            }
        }
    }

    private void updateScore()
    {
        Score.text = (Mathf.Floor(this.transform.position.z)*5).ToString();
    }
    private void checkIfGrounded()
    {
        isGrounded = Physics.CheckSphere(transform.position, 3,GroundLayer);
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
                }
                else
                {
                    if(slideCoroutine is null)
                    {
                        slideCoroutine = StartCoroutine(ScaleDownCoroutine());
                    }
                }

            }
        }
    }
    

    private void MoveRight()
    {
        if (playerCurrentState != playerPositionState.right)
        {
            //if currently in center move to right else center
            playerCurrentState = (playerCurrentState == playerPositionState.center) ? playerPositionState.right : playerPositionState.center;
        }
    }

    private void MoveLeft()
    {
        if (playerCurrentState != playerPositionState.left)
        {
            //if currently in center move to left else center
            playerCurrentState = (playerCurrentState == playerPositionState.center) ? playerPositionState.left : playerPositionState.center;
            
        }
    }

    private void jump()
    {
        if(isGrounded)
        {
            isGrounded = false;
            //playerRigidBody.AddForce(Vector3.up * jumpforce , ForceMode.Impulse);
            transform.position = new Vector3(transform.position.x, jumpforce, transform.position.z); 
        }
    }

    private IEnumerator ScaleDownCoroutine()
    {
        transform.localScale -= ScaleDownFactor;
        yield return new WaitForSeconds(1f);
        transform.localScale += ScaleDownFactor;
        slideCoroutine = null;
    }

    public void AddDamage()
    {
        if (playerHealth.currentHealth >= 1)
        {
            playerHealth.OnDamage();
            return;
        }

        gameObject.SetActive(false);
        gameManager.GameOver();
    }


    public void AddCoin()
    {
        coinAmount++;
        coinText.text = coinAmount.ToString();
    }
}

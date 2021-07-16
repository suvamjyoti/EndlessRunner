using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    private int NoOfLife;
    private int _currentHealth;
    public int currentHealth { get { return _currentHealth; } }

    [SerializeField] private Image[] heartsprites;
    [SerializeField] private BoxCollider playerCollider;

    private PlayerController playerController;

    private Vector3 initialPosition;


    private void Awake()
    {
        NoOfLife = heartsprites.Length;
        _currentHealth = NoOfLife;
        CalculateHealth();
    }

    private void Start()
    {

        playerController = GetComponent<PlayerController>();
        playerCollider = GetComponent<BoxCollider>();
    }


    public void OnDamage()
    {
        initialPosition = transform.position;
        _currentHealth--;
        CalculateHealth();

        // respawn a bit behind the position
        //Respawn();
    }


    //private void Respawn()
    //{
    //    StartCoroutine(RespawnAnimation());
    //}

    //private IEnumerator RespawnAnimation()
    //{
    //    playerCollider.enabled = false;
    //    yield return new WaitForSeconds(0.3f);
    //    playerController.enabled = false;
    //    yield return new WaitForSeconds(2);
    //    playerController.enabled = true;
    //}

    private void CalculateHealth()
    {
        //for(int i = 0; i < currentHealth; i++)
        //{
        //    heartsprites[i].sprite = heartSprite;
        //}

        if (_currentHealth < NoOfLife)
        {
            for(int i = _currentHealth; i < NoOfLife; i++)
            {
                //heartsprites[i].sprite = heartSprite;
                heartsprites[i].color = Color.black;
            }
        }

    }

}

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
    //[SerializeField] private Sprite heartSprite;

    private void Awake()
    {
        NoOfLife = heartsprites.Length;
        _currentHealth = NoOfLife;
        CalculateHealth();
    }


    public void OnDamage()
    {
        _currentHealth--;
        CalculateHealth();
    }

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

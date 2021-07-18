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
        if (_currentHealth < NoOfLife)
        {
            for(int i = _currentHealth; i < NoOfLife; i++)
            {
                heartsprites[i].color = Color.black;
            }
        }

    }

}

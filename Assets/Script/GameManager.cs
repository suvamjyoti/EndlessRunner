using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPrefab;

    public void GameOver()
    {
        GameObject overUI = Instantiate(gameOverPrefab, this.transform);
        transform.parent = overUI.transform;
    }

}
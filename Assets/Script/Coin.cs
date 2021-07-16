using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.gameObject.GetComponent<PlayerController>().AddCoin();
            StartCoroutine(reset());
        }

    }

    IEnumerator reset()
    {
        this.GetComponent<MeshRenderer>().enabled=false;
        yield return new WaitForSeconds(3f);
        this.GetComponent<MeshRenderer>().enabled = true;
    }

}

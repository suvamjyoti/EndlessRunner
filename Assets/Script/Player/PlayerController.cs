using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody characterRB;
    private float horizontal;

    private void Start()
    {
        characterRB = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        characterRB.transform.Translate(horizontal, 0, 1);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool onGround = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
        else if (other.gameObject.CompareTag("Target"))
        {
            Debug.Log("HIT!");
            onGround = true;
        }
    }

    private void Update()
    {
        if (onGround)
        {
            rb.velocity = Vector3.zero;
        }
    }
}

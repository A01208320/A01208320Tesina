using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    private bool onGround = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            Stop();
        } else if (other.gameObject.CompareTag("Target")) {
            Stop();
        }
    }

    private void Stop() {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        GetComponent<ParticleSystem>().Stop();
        Destroy(gameObject, 2);
    }
}

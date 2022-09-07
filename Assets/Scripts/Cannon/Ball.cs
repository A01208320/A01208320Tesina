using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody rb;
    private bool onGround = false;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (!onGround) {
            transform.forward = rb.velocity;
            GameManager.instance.ui.setDistanceA(Vector3.Distance(Vector3.zero, new Vector3(transform.position.x, 0, transform.position.z)).ToString());
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Ground")) {
            Stop();
        } else if (other.gameObject.CompareTag("Target")) {
            other.gameObject.GetComponent<Target>().gotHit();
            clean();
        }
    }

    private void Stop() {
        onGround = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        clean();
    }

    private void clean() {
        GameManager.instance.ballLanded();
        GameManager.instance.cam.setTarget(GameManager.instance.cannon.transform, new Vector3(0, 4, -3), 30, 0.3f, CameraManager.typeCam.cannon);
        Destroy(gameObject);
    }
}

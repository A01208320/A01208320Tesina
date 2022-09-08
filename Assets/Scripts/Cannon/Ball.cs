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
            if (GameManager.instance.difficulty == GameManager.Difficulty.free) {
                GameManager.instance.ui.setDistance(Vector3.Distance(Vector3.zero, new Vector3(transform.position.x, 0, transform.position.z)).ToString());
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        onGround = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if (GameManager.instance.difficulty == GameManager.Difficulty.free) {
            GameManager.instance.ui.setDistance(GameManager.instance.cannon.calc.ToString());
        }
        if (other.gameObject.CompareTag("Ground")) {
            if (GameManager.instance.difficulty == GameManager.Difficulty.free) {
                GameManager.instance.cam.targetPoint(transform.position, transform.position);
            } else {
                GameManager.instance.cam.targetPoint(transform.position, GameManager.instance.enemy.getSelected().position);
            }
            Destroy(gameObject, 2);
        } else if (other.gameObject.CompareTag("Target")) {
            other.gameObject.GetComponent<Enemy>().gotHit(transform.position);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstacle : MonoBehaviour {
    [SerializeField] private Vector3 finalPos;
    [SerializeField] private bool ablemove;
    private Vector3 velocity;

    private void Start() {
        ablemove = false;
    }

    private void Update() {
        if (ablemove) {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, finalPos, ref velocity, 0.1f);
            if (transform.localPosition == finalPos) {
                Destroy(this);
            }
        }
    }

    public void move() {
        ablemove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObstacle : MonoBehaviour {
    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 finalPos;
    [SerializeField] private float time;
    [SerializeField] private bool ablemove;

    private void Start() {
        time = 0;
        origin = transform.localPosition;
        ablemove = false;
    }

    private void Update() {
        if (ablemove) {
            time += Time.deltaTime / 2;
            transform.localPosition = Vector3.Lerp(origin, finalPos, time);
            if (transform.localPosition == finalPos) {
                Destroy(this);
            }
        }
    }

    public void move() {
        ablemove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatearoundobject : MonoBehaviour {
    public Transform target1, target2;

    public Vector3 offset;
    public Vector3 targetPosition;
    public float distance;

    void Start() {
    }
    /*
        void LateUpdate() {
            targetPosition = (target2.position - target1.position).normalized;
            transform.position = target1.position + targetPosition * -2 + offset;
            transform.LookAt(target2);

        }
    */
    private void LateUpdate() {
        targetPosition = (target2.position - target1.position).normalized;
        transform.position = target1.position + targetPosition * distance + offset;
        transform.LookAt(target2);
    }
}

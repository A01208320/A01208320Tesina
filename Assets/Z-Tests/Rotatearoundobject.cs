using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatearoundobject : MonoBehaviour {
    public GameObject target;
    public float speed = 5;

    public Vector3 offset;

    void Start() {
    }

    void LateUpdate() {
        // Look
        //var newRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, speed * Time.deltaTime);
        //transform.rotation = newRotation;
        transform.rotation = Quaternion.Euler(Vector3.right * 360 + target.transform.rotation.eulerAngles);
        //transform.rotation *= Quaternion.Euler(Vector3.down * 0);

        // Move
        Vector3 newPosition = target.transform.position - target.transform.forward * offset.z + target.transform.up * offset.y;
        //transform.position = Vector3.Slerp(transform.position, newPosition, Time.deltaTime * speed);
        transform.position = newPosition;
    }
}

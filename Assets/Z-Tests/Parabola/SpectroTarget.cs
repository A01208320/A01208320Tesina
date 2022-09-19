using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectroTarget : MonoBehaviour {
    public Transform target;
    public ParabolaSimulation data;

    private void Update() {
        transform.position = target.position;
        transform.LookAt(data.projectileForward);
    }
}

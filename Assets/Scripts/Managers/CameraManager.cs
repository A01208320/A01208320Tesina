using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Transform target;
    private Vector3 offset;
    private void Awake() {
        GameManager.instance.cam = this;
    }

    public void setTarget(Transform tar, Vector3 vec) {
        target = tar;
        offset = vec;
        transform.position = target.position + offset;
    }

    private void Update() {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 velocity;
    private void Awake() {
        GameManager.instance.cam = this;
    }
    private void Start() {
        velocity = Vector3.zero;
    }

    public void setTarget(Transform target, Vector3 offset) {
        this.target = target;
        this.offset = offset;
    }

    private void LateUpdate() {
        Vector3 posTarget = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref velocity, smoothTime);
        transform.LookAt(target);
    }
}

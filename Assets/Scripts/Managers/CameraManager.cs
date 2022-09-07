using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float angleOffset;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 posTarget;
    [SerializeField] private Quaternion angleTarget;

    public enum typeCam {
        none,
        cannon,
        ball,
        circlePoint
    }
    [SerializeField] private typeCam type;

    private void Awake() {
        GameManager.instance.cam = this;
    }
    private void Start() {
        velocity = Vector3.zero;
    }

    public void setTarget(Transform target, Vector3 offset, float angleOffset, float smoothTime, typeCam type) {
        this.target = target;
        this.offset = offset;
        this.angleOffset = angleOffset;
        this.smoothTime = smoothTime;
        this.type = type;
    }

    private void LateUpdate() {
        switch (type) {
            case typeCam.cannon:
                followCannon();
                break;
            case typeCam.ball:
                followBall();
                break;
            case typeCam.circlePoint:
                break;
            default:
                posTarget = transform.position;
                angleTarget = transform.rotation;
                break;
        }
        transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref velocity, smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, angleTarget, Time.deltaTime * 10);
    }

    private void followCannon() {
        posTarget = target.position + offset;
        angleTarget = Quaternion.Euler(Vector3.right * angleOffset);
    }

    private void followBall() {
        posTarget = target.position - target.forward * offset.z + target.up * offset.y;
        angleTarget = Quaternion.Euler(Vector3.right * 360 + target.rotation.eulerAngles);
    }

    private void followTarget() {
        transform.rotation = Quaternion.Euler(Vector3.right * 360 + target.rotation.eulerAngles);
        Vector3 posTarget = target.position - target.forward * offset.z + target.up * offset.y;
        transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref velocity, smoothTime);

    }

    private void targetCannon() {
        Vector3 posTarget = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref velocity, smoothTime);
        transform.rotation = Quaternion.Euler(Vector3.SmoothDamp(transform.rotation.eulerAngles, Vector3.zero, ref velocity, smoothTime));
    }

    private void targetBall() {
        Vector3 posTarget = (target.position + offset) - transform.forward;
        transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref velocity, smoothTime);
        transform.rotation = target.rotation;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 point;
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

    IEnumerator rotate(int wait) {
        yield return new WaitForSeconds(wait);
        GameManager.instance.ballLanded();
    }

    public void targetCannon() {
        target = GameManager.instance.cannon.transform;
        offset = new Vector3(0, 4, -3);
        angleOffset = 30;
        smoothTime = 0.3f;
        type = typeCam.cannon;
    }

    public void targetBall(Transform target) {
        this.target = target;
        offset = new Vector3(0, 1, -1);
        angleOffset = 50;
        smoothTime = 0.2f;
        type = typeCam.ball;
    }

    public void targetPoint(Vector3 point, int wait) {
        this.point = point;
        type = typeCam.circlePoint;
        StartCoroutine(rotate(wait));
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
                circlePoint();
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

    private void circlePoint() {

    }
}

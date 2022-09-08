using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 point, otherpoint;
    [SerializeField] private bool Pointtarget;
    [SerializeField] private float distance;
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
        endball,
    }
    [SerializeField] private typeCam type;

    private void Awake() {
        GameManager.instance.cam = this;
    }
    private void Start() {
        velocity = Vector3.zero;
    }

    IEnumerator endTurn() {
        yield return new WaitForSeconds(5.0f);
        distance *= -1;
        Vector3 t = point;
        point = otherpoint;
        otherpoint = t;
        Pointtarget = true;
        yield return new WaitForSeconds(10.0f);
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
        smoothTime = 0.15f;
        Pointtarget = false;
        type = typeCam.ball;
    }

    public void targetPoint(Vector3 point1, Vector3 point2) {
        point = point1;
        otherpoint = point2;
        offset = new Vector3(0, 1, 0);
        distance = 2;
        smoothTime = 0.5f;
        type = typeCam.endball;
        StartCoroutine(endTurn());
    }

    private void LateUpdate() {
        switch (type) {
            case typeCam.cannon:
                followCannon();
                break;
            case typeCam.ball:
                followBall();
                break;
            case typeCam.endball:
                targetPoint();
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

    private void targetPoint() {
        posTarget = point - (otherpoint - point).normalized * distance + offset;
        if (Pointtarget) {
            angleTarget = Quaternion.LookRotation(point - transform.position);
        } else {
            angleTarget = Quaternion.LookRotation(otherpoint - transform.position);
        }
    }
}

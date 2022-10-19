using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 point, otherpoint;
    [SerializeField] private bool Pointtarget;
    [SerializeField] private float distance;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Vector3 posTarget;
    [SerializeField] private Quaternion angleTarget;

    public enum typeCam {
        none,
        player,
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
        //GameManager.instance.ballLanded();
    }

    public void targetPlayer() {
        target = GameManager.instance.player.cameraPos;
        offset = Vector3.zero;
        smoothTime = 0;
        type = typeCam.player;
    }

    public void targetCannon(Vector3 point) {
        target = GameManager.instance.cannon.model;
        this.point = point;
        offset = new Vector3(0, 1, 1);
        smoothTime = 0.3f;
        type = typeCam.cannon;
    }

    public void targetBall(Transform target) {
        this.target = target;
        point = GameManager.instance.cannon.CameraPos.position;
        offset = new Vector3(0, 0, 0);
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
            case typeCam.player:
                followPlayer();
                break;
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
        if (smoothTime == 0) {
            transform.rotation = Quaternion.Lerp(transform.rotation, angleTarget, 1);
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, angleTarget, Time.deltaTime * 10);
    }

    private void followPlayer() {
        posTarget = target.position;
        angleTarget = target.parent.GetComponent<PlayerManager>().getRot();
    }

    private void followCannon() {
        posTarget = target.position - (Quaternion.Euler(0, target.eulerAngles.y, 0) * Vector3.forward) * offset.z + Vector3.up * offset.y;
        //angleTarget = Quaternion.Euler(Vector3.right * angleOffset);
        angleTarget = Quaternion.LookRotation(point - transform.position);
    }

    private void followBall() {
        /*
        Ball b = target.GetComponent<Ball>();
        posTarget = target.position - b.forward * offset.z + b.up * offset.y;
        angleTarget = b.rotation;
        */
        posTarget = point;
        angleTarget = Quaternion.LookRotation(target.position - transform.position);
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

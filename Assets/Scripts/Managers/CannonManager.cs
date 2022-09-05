using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {
    [SerializeField] private float aV;
    [SerializeField] private float aH;
    [SerializeField] private float V0;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Vector3 firingDirection;
    [SerializeField] private float timer;
    [SerializeField] private float count;
    [SerializeField] private bool movingCannon;

    private void Awake() {
        GameManager.instance.cannon = this;
    }

    private void Start() {
        GameManager.instance.cam.setTarget(this.gameObject.transform, new Vector3(0, 4, -4));
        GameManager.instance.ui.showPanelValues();
        firingPoint = transform.GetChild(0).transform;
        movingCannon = false;
    }

    private void Update() {
        if (movingCannon) {
            count += Time.deltaTime / timer;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(-aV, aH, 0), count);
            if (1.2f <= count) {
                movingCannon = false;
                Fire();
            }
        }
    }

    public void Fire() {
        firingDirection = (transform.position - firingPoint.position).normalized;
        GameObject g = Instantiate(ball, transform);
        GameManager.instance.cam.setTarget(g.transform, new Vector3(0, 4, -4));
        Rigidbody rb = g.GetComponent<Rigidbody>();
        rb.velocity = firingDirection * -V0;
    }

    public void moveCannon() {
        movingCannon = true;
        timer = 1;
        count = 0;
    }

    public void setVangle(float angle) {
        aV = angle;
    }
    public void setHangle(float angle) {
        aH = angle;
    }
    public void setinital_velocity(float velocity) {
        V0 = velocity;
    }

    public void setValues(float velocity, float angleV, float angleH) {
        V0 = velocity;
        aV = angleV;
        aH = angleH;
    }


}

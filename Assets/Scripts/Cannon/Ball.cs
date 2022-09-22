using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [Header("Ambient Values")]
    [SerializeField] private Vector3 origin;
    [SerializeField] private float angle;
    [SerializeField] private float gravity;
    [SerializeField] private float distance;

    [Header("Speed Values")]
    [SerializeField] private float V0;
    [SerializeField] private float Vx;
    [SerializeField] private float Vy;

    [Header("Time Values")]
    [SerializeField] private float duration;
    [SerializeField] private float elapsed;

    public Vector3 forward, up;
    public Quaternion rotation;
    private bool ableMove;
    private float angleH;


    public void init(int complex, float param1, float param2, float gravity, float distance, float angleH) {
        origin = transform.position;
        this.gravity = gravity;
        this.distance = distance;
        this.angleH = angleH;
        ableMove = false;

        if (complex == 0) { // Easy V0,a
            V0 = param1;
            angle = param2;
            Vx = V0 * Mathf.Cos(angle * Mathf.Deg2Rad);
            Vy = V0 * Mathf.Sin(angle * Mathf.Deg2Rad);
        } else { // complex Vx, Vy
            Vx = param1;
            Vy = param2;
        }

        duration = distance / Vx;
        elapsed = 0;
    }

    private void Update() {
        if (!ableMove) {
            return;
        }

        if (elapsed < duration) {
            Vector3 newPos = new Vector3(0, (Vy - (gravity * elapsed)) * Time.deltaTime, Vx * Time.deltaTime);

            forward = newPos.normalized;
            rotation = Quaternion.LookRotation(forward);
            up = rotation * Vector3.up;

            transform.Translate(newPos);

            elapsed += Time.deltaTime;
        } else {
            GameManager.instance.ui.setDP(distance.ToString());
            GameManager.instance.ballLanded();
            Destroy(this);
        }
        float dis = Vector3.Distance(origin, new Vector3(transform.position.x, 0, transform.position.z));
        if (distance <= dis) {
            elapsed = duration;
            dis = distance;
        }
        GameManager.instance.ui.setDP(dis.ToString());
    }

    public void startMoving() {
        ableMove = true;
        transform.localRotation = Quaternion.Euler(0, angleH, 0);
    }
}

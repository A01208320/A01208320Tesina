using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    [Header("Ambient Values")]
    [SerializeField] private float angle;
    [SerializeField] private float gravity;
    [SerializeField] private float distance;

    [Header("Speed Values")]
    [SerializeField] private float V0;
    [SerializeField] private float Vx;
    [SerializeField] private float Vy;
    [SerializeField] private Vector3 newPos;

    [Header("Time Values")]
    [SerializeField] private float duration;
    [SerializeField] private float elapsed;

    public Vector3 forward, up;
    public Quaternion rotation;
    private float angleH;


    public void init(float V0, float Vx, float Vy, float angle, float gravity, float distance, float angleH) {
        this.V0 = V0;
        this.Vx = Vx;
        this.Vy = Vy;
        this.angle = angle;
        this.gravity = gravity;
        this.distance = distance;
        this.angleH = angleH;
        duration = (2 * V0 * Mathf.Sin(angle * Mathf.Deg2Rad)) / gravity;
    }

    private IEnumerator move() {
        elapsed = 0;
        while (elapsed < duration) {
            float x = Vx * elapsed;
            float y = (Vy * elapsed) - (0.5f * gravity * elapsed * elapsed);
            newPos = new Vector3(0, y, x);
            transform.localPosition = transform.localRotation * newPos;

            Vector2 PosGround = new Vector2(transform.localPosition.x, transform.localPosition.z);
            float dis = Vector2.Distance(Vector2.zero, PosGround);
            GameManager.instance.ui.setDP(dis.ToString());

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = transform.localRotation * new Vector3(0, 0, distance);
        GameManager.instance.ui.setDP(distance.ToString());
        GameManager.instance.cannon.Targets.getTarget().GetComponent<Target>().check(distance, gameObject);

        yield return new WaitForSeconds(1f);
        GameManager.instance.ballLanded();
    }

    public void startMoving() {
        transform.localRotation = Quaternion.Euler(0, angleH, 0);
        StartCoroutine(move());
    }

    private void OnCollisionEnter(Collision other) {
        StopCoroutine(move());
        GameManager.instance.ballLanded();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {
    [Header("Cannon values")]
    [SerializeField] public Transform model;
    [SerializeField] public ButtonStartCannon button;
    [SerializeField] private int cannonNumber;
    [SerializeField] private float V0;
    [SerializeField] private float aV;
    [SerializeField] private float aH;
    [SerializeField] private float Vx;
    [SerializeField] private float Vy;
    [Header("Projectile")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] public TargetManager Targets;
    [SerializeField] public Transform CameraPos;
    [Header("Calculated")]
    [SerializeField] public float distance;
    [Header("Adjust difficulty")]
    [SerializeField][Range(0, 1)] private int complex;
    [SerializeField] private float gravity;
    [SerializeField] private bool fixed_FirstParam;
    [SerializeField] private float value_FirstParam;
    [SerializeField] private float min_FirstParam;
    [SerializeField] private float max_FirstParam;
    [SerializeField] private bool fixed_SecondParam;
    [SerializeField] private float value_SecondParam;
    [SerializeField] private float min_SecondParam;
    [SerializeField] private float max_SecondParam;
    private GameObject ball;

    public void init() {
        // Announce the GameManager the existance
        GameManager.instance.cannon = this;

        // Give values to UI
        GameManager.instance.ui.init(complex, fixed_FirstParam, value_FirstParam.ToString(), min_FirstParam, max_FirstParam, fixed_SecondParam, value_SecondParam.ToString(), min_SecondParam, max_SecondParam, gravity.ToString(), Targets.numTargets(), Targets.getDistance().ToString());

        // Start the cannon minigame
        aH = Targets.getAngle();
        StartCoroutine(moveCannon(false));
        GameManager.instance.startCannonGame();
    }

    public void exit() {
        GameManager.instance.cannon = null;

        bool finished = Targets.checkFinished();
        if (finished) {
            button.gameObject.layer = LayerMask.NameToLayer("Default");
            Destroy(button);
            Destroy(CameraPos.gameObject);
            Destroy(Targets);
            Destroy(this);
        }

        GameManager.instance.endCannonGame();
    }

    public void checkUI() {
        bool deleteBall = Targets.getTarget().GetComponent<Target>().check(distance);
        if (deleteBall) {
            Destroy(ball);
        }

        if (Targets.checkFinished()) {
            GameManager.instance.progression.activate(cannonNumber);
        }
        GameManager.instance.ui.checkUI(Targets.checkFinished());
    }

    public void nextTarget() {
        Targets.nextTarget();
        aH = Targets.getAngle();
        GameManager.instance.ui.setDT(Targets.getDistance().ToString());
        GameManager.instance.ui.ableShoot(Targets.isCompleted());
        GameManager.instance.cam.targetCannon(Targets.getTarget().position);
        StartCoroutine(moveCannon(false));
    }
    public void prevTarget() {
        Targets.prevTarget();
        aH = Targets.getAngle();
        GameManager.instance.ui.setDT(Targets.getDistance().ToString());
        GameManager.instance.ui.ableShoot(Targets.isCompleted());
        GameManager.instance.cam.targetCannon(Targets.getTarget().position);
        StartCoroutine(moveCannon(false));
    }

    private IEnumerator moveCannon(bool shot) {
        if (shot) {
        }
        Quaternion starting = model.localRotation;
        Quaternion target = Quaternion.Euler(-aV, aH, 0);

        float elapsed = 0;
        float time = 1;
        while (elapsed < time) {
            model.localRotation = Quaternion.Slerp(starting, target, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (shot) {
            distance = V0 * V0 * (Mathf.Sin(2 * aV * Mathf.Deg2Rad)) / gravity;
            ball = Instantiate(ballPrefab, transform.position, transform.rotation, transform);
            GameManager.instance.cannonShoot(ball.transform);
            yield return new WaitForSeconds(1);
            Fire();
        }
    }

    /*
        private void Update() {
            if (movingCannon) {
                count += Time.deltaTime / timer;
                model.localRotation = Quaternion.Slerp(model.localRotation, Quaternion.Euler(-aV, aH, 0), count);
                if (1.2f <= count) {
                    movingCannon = false;
                    if (shootCannon) {
                        shootCannon = false;
                        distance = V0 * V0 * (Mathf.Sin(2 * aV * Mathf.Deg2Rad)) / gravity;
                        Fire();
                    }
                }
            }
        }
    */

    public void Fire() {
        ball.GetComponent<Ball>().init(V0, Vx, Vy, aV, gravity, distance, aH);
        ball.GetComponent<Ball>().startMoving();
    }

    public void setValues(float param1, float param2) {
        if (complex == 0) {
            if (!fixed_FirstParam) {
                V0 = param1;
            } else {
                V0 = value_FirstParam;
            }

            if (!fixed_SecondParam) {
                aV = param2;
            } else {
                aV = value_SecondParam;
            }
            calcComponents();
        } else {
            if (!fixed_FirstParam) {
                Vx = param1;
            } else {
                Vx = value_FirstParam;
            }

            if (!fixed_SecondParam) {
                Vy = param2;
            } else {
                Vy = value_SecondParam;
            }
            calcDirection();
        }

        StartCoroutine(moveCannon(true));
    }

    private void calcDirection() {
        V0 = Vector2.Distance(new Vector2(Vx, Vy), Vector2.zero);
        aV = Mathf.Atan2(Vy, 0) * Mathf.Rad2Deg;
    }
    private void calcComponents() {
        Vx = V0 * Mathf.Cos(aV * Mathf.Deg2Rad);
        Vy = V0 * Mathf.Sin(aV * Mathf.Deg2Rad);
    }
}

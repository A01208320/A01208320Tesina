using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {
    [Header("Cannon values")]
    [SerializeField] public Transform model;
    [SerializeField] public ButtonStartCannon button;
    [SerializeField] private float V0;
    [SerializeField] private float aV;
    [SerializeField] private float aH;
    [SerializeField] private float Vx;
    [SerializeField] private float Vy;
    [Header("Projectile")]
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] public TargetManager Targets;
    [SerializeField] public Transform CameraPos;
    [SerializeField] private ActivateObjects act;
    [Header("Calculated")]
    [SerializeField] public float distance;
    [SerializeField] public float height;
    [Header("Adjust difficulty")]
    [SerializeField][Range(0, 1)] private int complex;
    [SerializeField] private bool random_gravity;
    [SerializeField] private float gravity;
    [SerializeField] private float min_gravity;
    [SerializeField] private float max_gravity;
    [SerializeField] private bool fixed_FirstParam;
    [SerializeField] private bool random_FirstParam;
    [SerializeField] private float value_FirstParam;
    [SerializeField] private float min_FirstParam;
    [SerializeField] private float max_FirstParam;
    [SerializeField] private bool fixed_SecondParam;
    [SerializeField] private bool random_SecondParam;
    [SerializeField] private float value_SecondParam;
    [SerializeField] private float min_SecondParam;
    [SerializeField] private float max_SecondParam;
    private GameObject ball;

    public void init() {
        // Announce the GameManager the existance
        GameManager.instance.cannon = this;

        // Calc random params
        randValues();

        // Give values to UI
        //GameManager.instance.ui.init(complex, fixed_FirstParam, value_FirstParam.ToString(), fixed_SecondParam, value_SecondParam.ToString(), gravity.ToString(), Targets.numTargets(), Targets.getDistance().ToString());

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
        if (Targets.checkFinished()) {
            act.activate();
        }
        GameManager.instance.ui.checkUI(Targets.checkFinished());
    }

    private void randValues() {
        if (random_gravity) {
            gravity = Random.Range(min_gravity, max_gravity);
        }
        if (fixed_FirstParam && random_FirstParam) {
            value_FirstParam = Random.Range(min_FirstParam, max_FirstParam);
        }
        if (fixed_SecondParam && random_SecondParam) {
            value_SecondParam = Random.Range(min_SecondParam, max_SecondParam);
        }

        GameManager.instance.ui.init(complex, fixed_FirstParam, value_FirstParam.ToString(), fixed_SecondParam, value_SecondParam.ToString(), gravity.ToString(), Targets.numTargets(), Targets.getDistance().ToString());
    }

    public void nextTarget() {
        Targets.nextTarget();
        prepTarget();
    }
    public void prevTarget() {
        Targets.prevTarget();
        prepTarget();
    }

    private void prepTarget() {
        aH = Targets.getAngle();
        randValues();
        //GameManager.instance.ui.setDT(Targets.getDistance().ToString());
        GameManager.instance.ui.adjustValues(complex, Targets.getDistance().ToString(), gravity.ToString(), value_FirstParam.ToString(), value_SecondParam.ToString());
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
            height = (V0 * V0 * Mathf.Pow(Mathf.Sin(aV * Mathf.Deg2Rad), 2.0f)) / (2 * gravity);
            ball = Instantiate(ballPrefab, transform.position, transform.rotation, transform);
            CameraPos.localPosition = Quaternion.Euler(0, aH, 0) * new Vector3(-2.0f, height, distance / 2.0f);
            GameManager.instance.cannonShoot(ball.transform);
            yield return new WaitForSeconds(1);
            Fire();
        }
    }

    public void Fire() {
        ball.GetComponent<Ball>().init(V0, Vx, Vy, aV, gravity, distance, aH);
        GameManager.instance.playSound(GameManager.Sound.CannonSound);
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

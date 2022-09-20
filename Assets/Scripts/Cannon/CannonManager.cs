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
    [Header("Projectile")]
    [SerializeField] private GameObject ball;
    [SerializeField] private TargetManager Targets;
    [SerializeField] public Transform CameraPos;
    [Header("Moving Object")]
    [SerializeField] private ClearObstacle moving;
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
    private float timer, count;
    private bool movingCannon, shootCannon;
    private GameObject g;

    private void Start() {
        movingCannon = false;
    }

    public void init() {
        // Announce the GameManager the existance
        GameManager.instance.cannon = this;

        // Give values to UI
        GameManager.instance.ui.init(complex, fixed_FirstParam, value_FirstParam.ToString(), min_FirstParam, max_FirstParam, fixed_SecondParam, value_SecondParam.ToString(), min_SecondParam, max_SecondParam, gravity.ToString(), Targets.numTargets(), Targets.getDistance().ToString());

        // Start the cannon minigame
        aH = Targets.getAngle();
        moveCannon(false);
        GameManager.instance.startCannonGame();
    }

    public void exit() {
        GameManager.instance.cannon = null;

        bool finished = Targets.checkFinished();
        if (finished) {
            button.gameObject.layer = LayerMask.NameToLayer("Default");
            button.Destroy();
            Destroy(CameraPos.gameObject);
            Destroy(Targets.gameObject);
            Destroy(this);
        }

        GameManager.instance.endCannonGame();
    }

    public void checkUI() {
        bool deleteBall = Targets.getTarget().GetComponent<Target>().check(distance);
        if (deleteBall) {
            Destroy(g);
        } else {
            g.transform.localPosition = Targets.getTarget().localPosition;
        }
        if (Targets.checkFinished()) {
            moving.move();
        }
        GameManager.instance.ui.checkUI(Targets.checkFinished(), 1 < Targets.numTargets());
    }

    public void nextTarget() {
        Targets.nextTarget();
        aH = Targets.getAngle();
        GameManager.instance.ui.setDT(Targets.getDistance().ToString());
        moveCannon(false);
    }
    public void prevTarget() {
        Targets.prevTarget();
        aH = Targets.getAngle();
        GameManager.instance.ui.setDT(Targets.getDistance().ToString());
        moveCannon(false);
    }

    private void Update() {
        // Move cannon orientation
        if (movingCannon) {
            count += Time.deltaTime / timer;
            model.rotation = Quaternion.Slerp(model.rotation, Quaternion.Euler(-aV, aH, 0), count);
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

    public void Fire() {
        if (complex == 0) {
            g.GetComponent<Ball>().init(complex, V0, aV, gravity, distance, aH);
        } else {
            g.GetComponent<Ball>().init(complex, 0, 0, gravity, distance, aH);
        }
        g.GetComponent<Ball>().startMoving();
    }

    public void moveCannon(bool shoot) {
        movingCannon = true;
        timer = 1;
        count = 0;

        shootCannon = shoot;
        if (shootCannon) {
            g = Instantiate(ball, transform.position, transform.rotation, transform);
            GameManager.instance.cannonShoot(g.transform);
        }
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
        } else {
            if (!fixed_FirstParam) {
                //TODO
            }
        }

        moveCannon(true);
    }
}

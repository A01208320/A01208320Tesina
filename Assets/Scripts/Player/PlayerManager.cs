using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    private Rigidbody rb;
    [SerializeField] public Transform cameraPos;
    [SerializeField] private Vector3 input, mouse;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float sensX, sensY;
    float rotX, rotY;
    [SerializeField] private bool interact;
    [SerializeField] private LayerMask buttonlayer;
    public bool ablemove;
    [SerializeField] private Transform feet;
    [SerializeField] private Transform Upfeet;
    [SerializeField] private float stepHeight;
    [SerializeField] private float stepSmooth;
    [SerializeField] private float disFeet;
    [SerializeField] private float disUpfeet;
    private RaycastHit HitLower, HitUpper, HitLower45P, HitUpper45P, HitLower45N, HitUpper45N;

    private void Awake() {
        GameManager.instance.player = this;
    }

    private void Start() {
        Upfeet.localPosition = new Vector3(Upfeet.localPosition.x, stepHeight, Upfeet.localPosition.z);
        rb = GetComponent<Rigidbody>();
        GameManager.instance.cam.targetPlayer();
        GameManager.instance.unlockPlayer();
    }

    private void Update() {
        if (!ablemove) {
            return;
        }
        getInput();
        setDirection();
        detectInteractable();
    }

    private void getInput() {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        mouse = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
        interact = Input.GetMouseButtonDown(0);
    }

    private void setDirection() {
        rotY += mouse.x;
        rotX = Mathf.Clamp(rotX - mouse.y, -90f, 90f);

        transform.rotation = Quaternion.Euler(0, rotY, 0);
    }


    private void detectInteractable() {
        RaycastHit hit;
        Ray ray = GameManager.instance.cam.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1.0f, buttonlayer)) {
            if (hit.transform.parent.GetComponent<ButtonStartCannon>() != null) {
                GameManager.instance.ui.setmouseactive(true);
                if (interact) {
                    hit.transform.parent.GetComponent<ButtonStartCannon>().startCannon();
                }
            }
        } else {
            GameManager.instance.ui.setmouseactive(false);
        }
    }

    private void FixedUpdate() {
        if (!ablemove) {
            return;
        }
        rb.velocity = ((transform.forward * input.z + transform.right * input.x) * movementSpeed) + Vector3.up * rb.velocity.y;
        stepClimb();
    }

    private void stepClimb() {
        if (input == Vector3.zero) {
            return;
        }

        if (Physics.Raycast(feet.position, transform.TransformDirection(Vector3.forward), out HitLower, disFeet)) {
            if (!Physics.Raycast(Upfeet.position, transform.TransformDirection(Vector3.forward), out HitUpper, disUpfeet)) {
                rb.position += new Vector3(0, stepSmooth, 0);
            }
        }

        if (Physics.Raycast(feet.position, transform.TransformDirection(1.5f, 0, 1), out HitLower45P, disFeet)) {
            if (!Physics.Raycast(Upfeet.position, transform.TransformDirection(1.5f, 0, 1), out HitUpper45P, disUpfeet)) {
                rb.position += new Vector3(0, stepSmooth, 0);
            }
        }

        if (Physics.Raycast(feet.position, transform.TransformDirection(-1.5f, 0, 1), out HitLower45N, disFeet)) {
            if (!Physics.Raycast(Upfeet.position, transform.TransformDirection(-1.5f, 0, 1), out HitUpper45N, disUpfeet)) {
                rb.position += new Vector3(0, stepSmooth, 0);
            }
        }
    }

    private void OnDrawGizmos() {
        if (HitLower.collider) {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.magenta;
        }
        Gizmos.DrawLine(feet.position, feet.position + transform.TransformDirection(Vector3.forward) * disFeet);
        Gizmos.DrawLine(Upfeet.position, Upfeet.position + transform.TransformDirection(Vector3.forward) * disUpfeet);
    }

    public Quaternion getRot() {
        return Quaternion.Euler(rotX, rotY, 0);
    }

    public void setMove(bool check) {
        ablemove = check;
        input = Vector2.zero;
        rb.velocity = Vector2.zero;
    }
}

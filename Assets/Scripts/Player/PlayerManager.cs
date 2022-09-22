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

    private void Awake() {
        GameManager.instance.player = this;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        GameManager.instance.cam.targetPlayer();
        GameManager.instance.lockCursor();
        ablemove = true;
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
        input = new Vector3(Input.GetAxisRaw("Horizontal"), rb.velocity.y, Input.GetAxisRaw("Vertical"));
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
        if (Physics.Raycast(ray, out hit, 3.0f, buttonlayer)) {
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
    }

    public Quaternion getRot() {
        return Quaternion.Euler(rotX, rotY, 0);
    }

    public void setMove(bool check) {
        ablemove = check;
    }
}

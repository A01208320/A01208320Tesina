using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField] public TMP_InputField Input_V0;
    [SerializeField] public TextMeshProUGUI Text_V0;
    [SerializeField] public TMP_InputField Input_a;
    [SerializeField] public TextMeshProUGUI Text_a;
    [SerializeField] public TMP_InputField Input_Vx;
    [SerializeField] public TextMeshProUGUI Text_Vx;
    [SerializeField] public TMP_InputField Input_Vy;
    [SerializeField] public TextMeshProUGUI Text_Vy;
    [SerializeField] public Button Button_Shoot;
    [SerializeField] public TextMeshProUGUI Text_DT;
    [SerializeField] public TextMeshProUGUI Text_G;
    [SerializeField] public TextMeshProUGUI Text_DP;
    [SerializeField] public Button Button_Return;
    [SerializeField] public Button Button_nextTarget;
    [SerializeField] public Button Button_prevTarget;
    private float complex;
    private bool finished = false;
    private bool multipleTargets;
    [SerializeField] public GameObject mouseicon;


    private void Awake() {
        GameManager.instance.ui = this;
        mouseicon.SetActive(false);
    }

    public void init(float complex, bool fixed1, string value1, bool fixed2, string value2, string gravity, int numTargets, string targetDistance) {
        this.complex = complex;
        Text_G.text = gravity;
        Text_DT.text = targetDistance;
        switch (complex) {
            case 0: // Easy, V0 a
                // Prepare V0
                if (fixed1) {
                    Input_V0.gameObject.SetActive(false);
                    Text_V0.gameObject.SetActive(true);
                    Text_V0.text = value1;
                } else {
                    Text_V0.gameObject.SetActive(false);
                    Input_V0.gameObject.SetActive(true);
                }

                // Prepare a
                if (fixed2) {
                    Input_a.gameObject.SetActive(false);
                    Text_a.gameObject.SetActive(true);
                    Text_a.text = value2;
                } else {
                    Text_a.gameObject.SetActive(false);
                    Input_a.gameObject.SetActive(true);
                }
                break;
            case 1: // Hard Vx, Vy
                // Prepare Vx
                if (fixed1) {
                    Input_Vx.gameObject.SetActive(false);
                    Text_Vx.gameObject.SetActive(true);
                    Text_Vx.text = value1;
                } else {
                    Text_Vx.gameObject.SetActive(false);
                    Input_Vx.gameObject.SetActive(true);
                }

                //Prepare Vy
                if (fixed1) {
                    Input_Vy.gameObject.SetActive(false);
                    Text_Vy.gameObject.SetActive(true);
                    Text_Vy.text = value2;
                } else {
                    Text_Vy.gameObject.SetActive(false);
                    Input_Vy.gameObject.SetActive(true);
                }
                break;
        }

        if (numTargets == 1) {
            multipleTargets = false;
        } else {
            multipleTargets = true;
        }
        finished = false;
    }

    public void adjustValues(int complex, string distanceTarget, string gravity, string firstParam, string secondParam) {
        Text_DT.text = distanceTarget;
        Text_G.text = gravity;

        if (complex == 0) {
            if (Text_V0.IsActive()) {
                Text_V0.text = firstParam;
            }

            if (Text_a.IsActive()) {
                Text_a.text = secondParam;
            }
        } else {
            if (Text_Vx.IsActive()) {
                Text_Vx.text = firstParam;
            }

            if (Text_Vy.IsActive()) {
                Text_Vy.text = secondParam;
            }
        }
    }

    public void setDT(string text) {
        Text_DT.text = text;
    }
    public void setDP(string text) {
        Text_DP.text = text;
    }

    public void showUI(bool show, bool exit) {
        activeInteractables(show);
        GetComponent<Animator>().SetFloat("Complex", complex);
        GetComponent<Animator>().SetBool("Exit", exit);
        GetComponent<Animator>().SetBool("Show", show);
    }

    public void ButtonPressed() {
        Button_Shoot.interactable = false;
        float input1, input2;
        if (complex == 0) {
            float.TryParse(Input_V0.text, out input1);
            float.TryParse(Input_a.text, out input2);
        } else {
            float.TryParse(Input_Vx.text, out input1);
            float.TryParse(Input_Vy.text, out input2);
        }
        GameManager.instance.playSound(GameManager.Sound.UI);
        GameManager.instance.cannon.setValues(input1, input2);
    }

    public void nextTarget() {
        GameManager.instance.playSound(GameManager.Sound.UI);
        GameManager.instance.cannon.nextTarget();
    }
    public void prevTarget() {
        GameManager.instance.playSound(GameManager.Sound.UI);
        GameManager.instance.cannon.prevTarget();
    }
    public void stopMinigame() {
        GameManager.instance.playSound(GameManager.Sound.UI);
        GameManager.instance.cannon.exit();
    }

    public void checkUI(bool finished) {
        this.finished = finished;
    }

    private void activeInteractables(bool set) {
        Input_V0.interactable = set;
        Input_Vx.interactable = set;
        Input_Vy.interactable = set;
        Input_a.interactable = set;
        Button_Return.interactable = set;

        if (set && finished) {
            Button_Shoot.interactable = false;
        } else {
            Button_Shoot.interactable = set;
        }

        if (set && !multipleTargets) {
            Button_nextTarget.interactable = false;
            Button_prevTarget.interactable = false;
        } else {
            Button_nextTarget.interactable = set;
            Button_prevTarget.interactable = set;
        }
    }
    public void ableShoot(bool completed) {
        if (finished || completed) {
            Button_Shoot.interactable = false;
        } else {
            Button_Shoot.interactable = true;
        }

    }

    public void setmouseactive(bool set) {
        mouseicon.SetActive(set);
    }

}

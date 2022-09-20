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

    public void init(float complex, bool fixed1, string value1, float min_limit1, float max_limit1, bool fixed2, string value2, float min_limit2, float max_limit2, string gravity, int numTargets, string targetDistance) {
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
                    Input_V0.GetComponent<Text_Validation>().setLimits(min_limit1, max_limit1);
                }

                // Prepare a
                if (fixed2) {
                    Input_a.gameObject.SetActive(false);
                    Text_a.gameObject.SetActive(true);
                    Text_a.text = value2;
                } else {
                    Text_a.gameObject.SetActive(false);
                    Input_a.gameObject.SetActive(true);
                    Input_a.GetComponent<Text_Validation>().setLimits(min_limit2, max_limit2);
                }
                break;
            case 1: // Hard Vx, Vy
                break;
        }

        if (numTargets == 1) {
            multipleTargets = false;
        } else {
            multipleTargets = true;
        }
        finished = false;
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
        float input1, input2;
        if (complex == 0) {
            float.TryParse(Input_V0.text, out input1);
            float.TryParse(Input_a.text, out input2);
        } else {
            float.TryParse(Input_Vx.text, out input1);
            float.TryParse(Input_Vy.text, out input2);
        }
        GameManager.instance.cannon.setValues(input1, input2);
    }

    public void nextTarget() {
        GameManager.instance.cannon.nextTarget();
    }
    public void prevTarget() {
        GameManager.instance.cannon.prevTarget();
    }
    public void stopMinigame() {
        GameManager.instance.cannon.exit();
    }

    public void checkUI(bool finished, bool multipleTargets) {
        this.finished = finished;
        this.multipleTargets = multipleTargets;
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

    public void setmouseactive(bool set) {
        mouseicon.SetActive(set);
    }

}

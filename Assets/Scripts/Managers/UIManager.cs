using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] public Panel_Values PanelValues;
    [SerializeField] public Panel_Distance PanelDistance;

    private void Awake() {
        GameManager.instance.ui = this;
    }

    public void showPanelValues() {
        PanelValues.GetComponent<Animator>().SetBool("Show", true);
        PanelDistance.GetComponent<Animator>().SetBool("Show", false);
    }
    public void showPanelDistance() {
        PanelDistance.GetComponent<Animator>().SetBool("Show", true);
        PanelValues.GetComponent<Animator>().SetBool("Show", false);
    }

    public void setDistanceA(string distance) {
        PanelDistance.setDistanceReal(distance);
    }
    public void setDistanceC(string distance) {
        PanelDistance.setDistanceCalc(distance);
    }
}

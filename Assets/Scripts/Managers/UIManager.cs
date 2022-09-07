using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    [SerializeField] public Panel_Values PanelValues;
    [SerializeField] public Panel_Distance PanelDistance;

    private void Awake() {
        GameManager.instance.ui = this;
    }

    public void showUI(bool show) {
        GetComponent<Animator>().SetBool("Show", show);
    }

    public void setDistance(string distance) {
        PanelDistance.setDistance(distance);
    }
}

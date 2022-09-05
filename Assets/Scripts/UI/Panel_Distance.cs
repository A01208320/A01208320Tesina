using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Panel_Distance : MonoBehaviour {
    [SerializeField] TextMeshProUGUI DistanciaReal;
    [SerializeField] TextMeshProUGUI DistanciaCalc;

    public void setDistanceReal(string text) {
        DistanciaReal.text = text;
    }
    public void setDistanceCalc(string text) {
        DistanciaCalc.text = text;
    }
}

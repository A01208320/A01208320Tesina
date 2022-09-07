using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Panel_Distance : MonoBehaviour {
    [SerializeField] TextMeshProUGUI Distancia;

    public void setDistance(string text) {
        Distancia.text = text;
    }
}

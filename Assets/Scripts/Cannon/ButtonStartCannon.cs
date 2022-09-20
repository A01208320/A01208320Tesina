using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartCannon : MonoBehaviour {
    private void Start() {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void startCannon() {
        GetComponent<Renderer>().material.color = Color.green;
        transform.parent.GetComponent<CannonManager>().init();
    }

    public void Destroy() {
        GetComponent<Renderer>().material.color = Color.gray;
    }
}

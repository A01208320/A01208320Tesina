using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartCannon : MonoBehaviour {

    public void startCannon() {
        transform.parent.GetComponent<CannonManager>().init();
    }
}

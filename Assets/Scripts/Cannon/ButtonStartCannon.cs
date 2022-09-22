using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStartCannon : MonoBehaviour {
    private Animator anim;
    private void Start() {
        anim = GetComponent<Animator>();
    }

    public void startCannon() {
        anim.SetTrigger("Activate");
        transform.parent.GetComponent<CannonManager>().init();
    }
}

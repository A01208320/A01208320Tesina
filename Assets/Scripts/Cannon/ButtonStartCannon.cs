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
        StartCoroutine(activate());
    }

    private IEnumerator activate() {
        GameManager.instance.lockPlayer();
        GameManager.instance.playSound(GameManager.Sound.Lever);
        yield return new WaitForSeconds(1);
        transform.parent.GetComponent<CannonManager>().init();
    }
}

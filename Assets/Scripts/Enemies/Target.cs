using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    private void Start() {
        GetComponent<ParticleSystem>().Stop();
    }

    public void gotHit() {
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 3);
    }
}

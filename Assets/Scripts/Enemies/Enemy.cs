using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private void Start() {
        GetComponent<ParticleSystem>().Stop();
    }

    public void gotHit() {
        GetComponent<ParticleSystem>().Play();
        GameManager.instance.cam.targetPoint(transform.position, 2);
        Destroy(gameObject, 2);
    }
}

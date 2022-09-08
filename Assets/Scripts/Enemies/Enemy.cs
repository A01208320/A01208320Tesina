using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    private void Start() {
        GetComponent<ParticleSystem>().Stop();
    }

    public void gotHit(Vector3 point) {
        GetComponent<ParticleSystem>().Play();
        GameManager.instance.cam.targetPoint(point, transform.position);
        Destroy(gameObject, 2);
    }
}

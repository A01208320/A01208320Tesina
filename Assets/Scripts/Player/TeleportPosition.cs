using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPosition : MonoBehaviour {
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            other.transform.position = transform.GetChild(0).transform.position;
        }
    }
}

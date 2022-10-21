using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour {
    [SerializeField] private Loader.Scene scene;

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Player")) {
            GameManager.instance.LoadScene(scene);
        }
    }
}

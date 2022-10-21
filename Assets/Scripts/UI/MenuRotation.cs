using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRotation : MonoBehaviour {
    [SerializeField] private float speedRotation;
    private void Update() {
        transform.Rotate(0, speedRotation * Time.deltaTime, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] private bool isCorrect;
    [SerializeField] private float distance;

    private void Start() {
        distance = Vector3.Distance(transform.position, transform.parent.position);
    }

    public bool check(float distanceBall) {
        if (!isCorrect) {
            return true;
        }
        float real = Mathf.Abs(distance - distanceBall);
        if (real <= 0.01f) {
            transform.parent.GetComponent<TargetManager>().setCorrect();
            Destroy(gameObject, 0.1f);
            return false;
        }
        return true;
    }
}

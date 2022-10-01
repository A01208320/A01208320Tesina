using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [Header("Distance")]
    [SerializeField] private bool randomDistance;
    [SerializeField] private float distance;
    [SerializeField] private float min_distance;
    [SerializeField] private float max_distance;
    [Header("Angle")]
    [SerializeField] private bool randomAngle;
    [SerializeField] private float angle;
    [SerializeField] private float min_angle;
    [SerializeField] private float max_angle;
    [Header("Control")]
    [SerializeField] private bool isCorrect;
    [SerializeField] private bool valid;

    private void Start() {
        if (randomDistance) {
            distance = Random.Range(min_distance, max_distance);
        }
        if (randomAngle) {
            angle = Random.Range(min_angle, max_angle);
        }
        float x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector3 pos = new Vector3(z, 0, x);
        transform.localPosition = pos;
        valid = true;
    }

    public bool check(float distanceBall) {
        if (!isCorrect || !valid) {
            return true;
        }
        float real = Mathf.Abs(distance - distanceBall);
        if (real <= 0.01f) {
            transform.parent.GetComponent<TargetManager>().setCorrect();
            valid = false;
            return false;
        }
        return true;
    }

    public bool isValid() {
        return valid;
    }
}

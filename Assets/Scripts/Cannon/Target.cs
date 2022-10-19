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
    [Header("Particles")]
    [SerializeField] private ParticleSystem particleCorrect;
    [SerializeField] private ParticleSystem particleIncorrect;

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
        if (!valid) {
            return true;
        }
        float real = Mathf.Abs(distance - distanceBall);
        if (real <= 0.01f) {
            if (isCorrect) {
                particleCorrect.Play();
            } else {
                particleIncorrect.Play();
            }
            transform.parent.GetComponent<TargetManager>().setCorrect();
            valid = false;
            return false;
        }
        return true;
    }

    public bool isValid() {
        return valid;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        float x, z;

        if (randomDistance && randomAngle) {
            x = min_distance * Mathf.Cos(min_angle * Mathf.Deg2Rad);
            z = min_distance * Mathf.Sin(min_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = min_distance * Mathf.Cos(max_angle * Mathf.Deg2Rad);
            z = min_distance * Mathf.Sin(max_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = min_distance * Mathf.Cos(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            z = min_distance * Mathf.Sin(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);

            x = max_distance * Mathf.Cos(min_angle * Mathf.Deg2Rad);
            z = max_distance * Mathf.Sin(min_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = max_distance * Mathf.Cos(max_angle * Mathf.Deg2Rad);
            z = max_distance * Mathf.Sin(max_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = max_distance * Mathf.Cos(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            z = max_distance * Mathf.Sin(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);

        } else if (randomDistance) {
            x = min_distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = min_distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = max_distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = max_distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
        } else if (randomAngle) {
            x = distance * Mathf.Cos(min_angle * Mathf.Deg2Rad);
            z = distance * Mathf.Sin(min_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = distance * Mathf.Cos(max_angle * Mathf.Deg2Rad);
            z = distance * Mathf.Sin(max_angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
            x = distance * Mathf.Cos(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            z = distance * Mathf.Sin(((max_angle + min_angle) / 2) * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);

        } else {
            x = distance * Mathf.Cos(angle * Mathf.Deg2Rad);
            z = distance * Mathf.Sin(angle * Mathf.Deg2Rad);
            Gizmos.DrawWireSphere(transform.parent.TransformPoint(transform.parent.localPosition + new Vector3(z, 0, x)), 0.2f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPoints : MonoBehaviour {
    public float horizontalOffset;
    public float verticalOffset;

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position + transform.forward, 0.2f);
        Gizmos.DrawWireSphere(transform.position + transform.up, 0.2f);

        Gizmos.color = Color.red;
        Vector3 EulerRot = transform.rotation.eulerAngles;
        Quaternion rot = Quaternion.Euler(0, EulerRot.y, 0);
        Vector3 alwaysFront = rot * Vector3.forward;
        Vector3 horizontalPosition = transform.position - alwaysFront * horizontalOffset + Vector3.up * verticalOffset;
        Gizmos.DrawWireSphere(horizontalPosition, 0.2f);
        Gizmos.color = Color.blue;
        Vector3 horizontalverticalPosition = transform.position - transform.forward * horizontalOffset + transform.up * verticalOffset;
        Gizmos.DrawWireSphere(horizontalverticalPosition, 0.2f);
    }
}

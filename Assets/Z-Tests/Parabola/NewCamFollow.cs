using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCamFollow : MonoBehaviour {
    public Transform target, relativePoint;
    public ParabolaSimulation data;
    public Vector3 offset;

    public Vector3 desiredPosition;
    public float distance;

    public Vector3 targetPos, targetDir, targetForward, targetUp;
    public Quaternion targetRotation, desiredRotation;
    public float offsetAngle;

    private void LateUpdate() {
        targetPos = target.position;
        targetForward = data.projectileForward;

        targetRotation = Quaternion.LookRotation(targetForward);
        targetUp = targetRotation * Vector3.up;

        desiredPosition = targetPos - targetForward * offset.z + targetUp * offset.y;

        transform.position = desiredPosition;
        transform.rotation = targetRotation;
    }
}

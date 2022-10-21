using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    [SerializeField] private int numbercorrect;
    [SerializeField] private int index = 0;
    [SerializeField] private int corrects;
    private int actualChildren, numChildren;

    private void Start() {
        actualChildren = numChildren = transform.childCount;

    }

    public Transform nextTarget() {
        index = (index + 1) % transform.childCount;
        return transform.GetChild(index);
    }

    public Transform prevTarget() {
        index--;
        if (index < 0) {
            index = transform.childCount - 1;
        }
        return transform.GetChild(index);
    }

    public void setCorrect() {
        corrects++;
    }

    public bool checkFinished() {
        return numbercorrect <= corrects;
    }

    public int numTargets() {
        return transform.childCount;
    }

    public Transform getTarget() {
        return transform.GetChild(index).transform;
    }
    public float getAngle() {
        return Quaternion.LookRotation(transform.GetChild(index).localPosition - transform.localPosition).eulerAngles.y;
    }
    public float getDistance() {
        return Vector3.Distance(transform.localPosition, transform.GetChild(index).localPosition);
    }
    public bool isCompleted() {
        return !transform.GetChild(index).GetComponent<Target>().isValid();
    }
}

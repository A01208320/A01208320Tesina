using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjects : MonoBehaviour {
    [SerializeField] private Material ButtonActivated;
    [SerializeField] private Material ConduitActivated;

    [SerializeField] private Renderer ButtonConduit;

    [SerializeField] private List<Transform> Objects;
    [SerializeField] private List<Vector3> relPosObjects;

    private void recolorObjects() {
        ButtonConduit.material = ButtonActivated;
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            for (int j = 0; j < child.childCount; j++) {
                Transform model = child.GetChild(j);
                if (model.CompareTag("Conduit")) {
                    model.GetComponent<Renderer>().material = ConduitActivated;
                }
            }
        }
    }

    private IEnumerator moveObject(Transform obj, Vector3 desiredPos) {
        Vector3 starting = obj.localPosition;
        obj.gameObject.SetActive(true);
        float elapsed = 0;
        float time = 5;
        while (elapsed < time) {
            obj.localPosition = Vector3.Lerp(starting, desiredPos, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.localPosition = desiredPos;
    }

    private void moveObjects() {
        for (int i = 0; i < Objects.Count; i++) {
            StartCoroutine(moveObject(Objects[i], Objects[i].localPosition + relPosObjects[i]));
        }
    }

    public void activate() {
        recolorObjects();
        moveObjects();
    }
}

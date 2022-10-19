using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour {
    [SerializeField] private TransformList moveObjects;
    [SerializeField] private Vector3List posObjects;
    [SerializeField] private RendererList recolorObjects;
    [SerializeField] private MaterialList colorObjects;

    private void Awake() {
        GameManager.instance.progression = this;
    }

    public void activate(int i) {
        if (0 < moveObjects.TransformLists.Count) {
            List<Transform> transforms = moveObjects.TransformLists[i].Transforms;
            List<Vector3> positions = posObjects.Vector3Lists[i].Vector3s;
            for (int a = 0; a < transforms.Count; a++) {
                StartCoroutine(moveObject(transforms[a], positions[a]));
            }
        }

        if (0 < recolorObjects.RendererLists.Count) {
            List<Renderer> renderers = recolorObjects.RendererLists[i].Renderers;
            List<Material> materials = colorObjects.MaterialLists[i].Materials;
            for (int a = 0; a < renderers.Count; a++) {
                renderers[a].material = materials[a];
            }
        }
    }

    private IEnumerator moveObject(Transform obj, Vector3 final) {
        Vector3 starting = obj.localPosition;
        obj.gameObject.SetActive(true);
        float elapsed = 0;
        float time = 5;
        while (elapsed < time) {
            obj.localPosition = Vector3.Lerp(starting, final, elapsed / time);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}

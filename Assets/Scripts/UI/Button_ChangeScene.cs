using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeScene : MonoBehaviour {
    [SerializeField] private Loader.Scene scene;

    public void loadScene() {
        GameManager.instance.LoadScene(scene);
    }
}

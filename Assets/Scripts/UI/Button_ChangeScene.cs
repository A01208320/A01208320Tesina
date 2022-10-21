using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeScene : MonoBehaviour {
    [SerializeField] private Loader.Scene scene;

    public void loadScene() {
        if (scene.ToString() == "Exit") {
            Application.Quit();
        }
        GameManager.instance.playSound(GameManager.Sound.UI);
        GameManager.instance.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_ChangeScene : MonoBehaviour {
    [SerializeField] private Loader.Scene scene;
    [SerializeField] private GameManager.Difficulty difficulty;

    public void loadScene() {
        GameManager.instance.difficulty = difficulty;
        GameManager.instance.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene {
        DevMenu,
        Cannon_Practice_Free,
        Cannon_Practice_Target,
        Cannon_Practice_Exam,
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}

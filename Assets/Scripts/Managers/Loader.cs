using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene {
        DevMenu,
        Cannon_Practice_Free,
        Cannon_Practice_Train,
        Cannon_Practice_Exam,
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}

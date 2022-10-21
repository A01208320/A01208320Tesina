using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene {
        Main_Menu,
        Level0,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Credis,
        practice,
        Exit
    }

    public static void Load(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }
}

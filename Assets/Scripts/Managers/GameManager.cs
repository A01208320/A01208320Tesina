using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance { get; private set; }
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }
    public enum Difficulty { none, free, practice, easy, medium, hard }

    public CannonManager cannon;
    public CameraManager cam;
    public UIManager ui;
    public EnemyManager enemy;

    public Difficulty difficulty;


    public void LoadScene(Loader.Scene scene) {
        Loader.Load(scene);
    }

    public void startGame() {
        cam.targetCannon();
        ui.showUI(true);
        enemy.step();
    }

    public void cannonShoot(float V0, float av) {
        ui.PanelValues.disableInteractable();
        ui.showUI(false);
        cannon.setValues(V0, av, 0);
        cannon.moveCannon();
    }

    public void ballLanded() {
        enemy.step();
        cam.targetCannon();
        ui.PanelValues.enableInteractable();
        ui.showUI(true);
    }

}

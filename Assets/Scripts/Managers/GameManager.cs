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
            DontDestroyOnLoad(this);
        }
    }
    public enum Difficulty { none, free, practice, easy, medium, hard }

    public PlayerManager player;
    public CannonManager cannon;
    public CameraManager cam;
    public UIManager ui;

    public Difficulty difficulty;


    public void LoadScene(Loader.Scene scene, Difficulty diff) {
        difficulty = diff;
        Loader.Load(scene);
    }

    public void startCannonGame() {
        unlockCursor();
        player.setMove(false);
        cam.targetCannon();
        ui.setmouseactive(false);
        ui.showUI(true, false);
    }

    public void endCannonGame() {
        lockCursor();
        player.setMove(true);
        cam.targetPlayer();
        ui.showUI(false, true);
    }

    public void cannonShoot(Transform target) {
        ui.showUI(false, false);
        cam.targetBall(target);
    }

    public void ballLanded() {
        cannon.checkUI();
        cam.targetCannon();
        ui.showUI(true, false);
    }

    public void lockCursor() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void unlockCursor() {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

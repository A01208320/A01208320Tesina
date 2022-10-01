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

    public PlayerManager player;
    public CannonManager cannon;
    public CameraManager cam;
    public UIManager ui;
    public ProgressionManager progression;


    public void LoadScene(Loader.Scene scene) {
        Loader.Load(scene);
    }

    public void startCannonGame() {
        cam.targetCannon(cannon.Targets.getTarget().position);
        ui.showUI(true, false);
        ui.ableShoot(cannon.Targets.isCompleted());
    }

    public void endCannonGame() {
        unlockPlayer();
        cam.targetPlayer();
        ui.showUI(false, true);
    }

    public void cannonShoot(Transform target) {
        ui.showUI(false, false);
        cam.targetBall(target);
    }

    public void ballLanded() {
        cannon.checkUI();
        cam.targetCannon(cannon.Targets.getTarget().position);
        ui.showUI(true, false);
        ui.ableShoot(cannon.Targets.isCompleted());
    }

    public void lockPlayer() {
        player.setMove(false);
        ui.setmouseactive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void unlockPlayer() {
        player.setMove(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}

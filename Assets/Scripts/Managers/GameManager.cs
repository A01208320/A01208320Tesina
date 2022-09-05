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

    public CannonManager cannon;
    public CameraManager cam;
    public UIManager ui;
    public EnemyManager enemy;

    private bool cannonTurn = true;

    public void LoadScene(Loader.Scene scene) {
        Loader.Load(scene);
    }

    public void cannonShoot(float V0, float av, float ah) {
        ui.PanelValues.disableInteractable();
        ui.showPanelDistance();
        cannon.setValues(V0, av, ah);
        cannon.moveCannon();
    }

    public void ballLanded() {
        ui.PanelValues.enableInteractable();
        ui.showPanelValues();
    }


}

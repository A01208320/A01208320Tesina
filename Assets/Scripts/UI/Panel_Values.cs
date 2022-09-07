using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_Values : MonoBehaviour {
    [SerializeField] TMP_InputField Text_V0;
    [SerializeField] TMP_InputField Text_a0;
    [SerializeField] public Button Button_Shoot;

    public void shoot() {
        float V0, a0;
        float.TryParse(Text_V0.text, out V0);
        if (V0 < 10) {
            V0 = 10;
            Text_V0.text = V0.ToString();
        }
        float.TryParse(Text_a0.text, out a0);
        if (a0 < 10) {
            a0 = 10;
            Text_a0.text = a0.ToString();
        }
        GameManager.instance.cannonShoot(V0, a0);
    }

    public void disableInteractable() {
        Button_Shoot.interactable = false;
        Text_V0.interactable = false;
        Text_a0.interactable = false;
    }

    public void enableInteractable() {
        Button_Shoot.interactable = true;
        Text_V0.interactable = true;
        Text_a0.interactable = true;
    }
}

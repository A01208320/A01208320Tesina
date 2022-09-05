using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Panel_Values : MonoBehaviour {
    [SerializeField] TMP_InputField Text_V0;
    [SerializeField] TMP_InputField Text_a0;
    [SerializeField] TMP_InputField Text_H0;
    [SerializeField] public Button Button_Shoot;

    public void shoot() {
        float V0 = float.Parse(Text_V0.text);
        float a0 = float.Parse(Text_a0.text);
        float ah = float.Parse(Text_H0.text);
        GameManager.instance.cannonShoot(V0, a0, ah);
    }

    public void disableInteractable() {
        Button_Shoot.interactable = false;
        Text_V0.interactable = false;
        Text_a0.interactable = false;
        Text_H0.interactable = false;
    }

    public void enableInteractable() {
        Button_Shoot.interactable = true;
        Text_V0.interactable = true;
        Text_a0.interactable = true;
        Text_H0.interactable = true;
    }
}

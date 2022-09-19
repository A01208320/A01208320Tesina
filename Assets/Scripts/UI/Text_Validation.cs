using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Validation : MonoBehaviour {
    private TMP_InputField input;
    private float min, max;

    private void Start() {
        input = GetComponent<TMP_InputField>();
    }

    public void setLimits(float min, float max) {
        this.min = min;
        this.max = max;
    }

    public void validate() {
        float num;
        string text = input.text;
        float.TryParse(text, out num);
        text = Mathf.Clamp(num, min, max).ToString();
        input.text = text;
    }
}

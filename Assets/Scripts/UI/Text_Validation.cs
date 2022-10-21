using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Validation : MonoBehaviour {
    private TMP_InputField input;

    private void Start() {
        input = GetComponent<TMP_InputField>();
    }

    public void validate() {
        float num;
        string text = input.text;
        float.TryParse(text, out num);
        input.text = num.ToString();
    }
}

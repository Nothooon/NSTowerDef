using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeSliderValue : MonoBehaviour
{
    public Slider slider;
    private TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void ChangeTextBasedOnValue()
    {
        int displayedValue = (int) Math.Round(slider.value * 100, 0);
        textBox.text = "" + displayedValue;
    }
}

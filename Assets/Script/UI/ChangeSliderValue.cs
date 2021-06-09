using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChangeSliderValue : MonoBehaviour
{
    public Slider slider; // Slider corresponding to the value we want to change
    private TextMeshProUGUI textBox; // Text component of the game object corresponding to the displayed value
    int displayedValue; // Rounded value displayed on screen

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("OptionsMenu") != null){ // As Options Menu isn't always active, we only act when it is active
            textBox = this.GetComponent<TextMeshProUGUI>();
            displayedValue = (int) Math.Round(slider.value * 100, 0); // Rounding the slider's value
            textBox.text = "" + displayedValue; // Changing the text
        }
    }

    // Called each time the slider's value changes
    public void ChangeTextBasedOnValue()
    {
        if (GameObject.Find("OptionsMenu") != null){ // As Options Menu isn't always active, we only act when it is active
            displayedValue = (int) Math.Round(slider.value * 100, 0); // Rounding the slider's value
            textBox.text = "" + displayedValue; // Changing the value
        }
    }
}

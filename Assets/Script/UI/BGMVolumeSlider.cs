using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BGMVolumeSlider : MonoBehaviour
{
    public Slider bgmSlider;
    public AudioSource music;
    public GameObject volumeText;
    
    void Start()
    {
        bgmSlider.wholeNumbers = true;
        bgmSlider.minValue = 0;
        bgmSlider.maxValue = 100;
        bgmSlider.value = 100;
    }

    public void OnValueChanged()
    {
        TextMeshProUGUI textComponent = volumeText.GetComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        textComponent.text = ""+bgmSlider.value;
        music.volume = bgmSlider.value / 100;
    }
}

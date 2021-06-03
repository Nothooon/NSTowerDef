using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Audio;
public class OptionsManager : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public GameObject sliderMusic;
    public GameObject sliderSound;
    public AudioMixer audioMixer;

    public void Start(){
        
        setMusicVolume(sliderMusic.GetComponent<Slider>().value);
        setSoundVolume(sliderSound.GetComponent<Slider>().value);

        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height}).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolution = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height){
                currentResolution = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex){
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setFullScreen(bool isFullScreen){
        Screen.fullScreen = isFullScreen;
    }

    public void setMusicVolume(float volume)
    {
        float dbVolume = Mathf.Log10(volume) * 20;
        if(volume == 0.0f){
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("Music", dbVolume);
    }

    public void setSoundVolume(float volume)
    {
        float dbVolume = Mathf.Log10(volume) * 20;
        if(volume == 0.0f){
            dbVolume = -80.0f;
        }
        audioMixer.SetFloat("SoundEffects", dbVolume);
    }
}
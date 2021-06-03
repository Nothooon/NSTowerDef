using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioMixerGroup soundEffectMixer;


    public void playMusic(AudioClip music){
        musicSource.PlayOneShot(music);
    }
     public void playSound(AudioClip sound){
        soundSource.PlayOneShot(sound);
    }

    public void PlayClipBeforeDestroy(AudioClip sound){
        Vector3 position = new Vector3(0,0,0);
        GameObject tempGameObject = new GameObject("TempAudio");
        tempGameObject.transform.position = position;
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>();
        audioSource.clip = sound;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGameObject, sound.length);
    }
}

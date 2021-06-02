using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class audioManager : MonoBehaviour
{
    
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioClip paperSound;


    public void playMusic(AudioClip music){
        musicSource.PlayOneShot(music);
    }
     public void playSound(AudioClip sound){
        soundSource.PlayOneShot(sound);
    }
}

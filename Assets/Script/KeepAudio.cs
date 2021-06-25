using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAudio : MonoBehaviour
{
    private static KeepAudio instance;
    public GameObject[] objects;

    void Awake(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 200;
        DontDestroyOnLoad(transform.gameObject);
        if(instance != null){
            Destroy(this.gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

        foreach(GameObject audio in objects){
                DontDestroyOnLoad(audio);
        }
    }
}

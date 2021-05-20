using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAudio : MonoBehaviour
{
    private static KeepAudio instance;
    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        if(instance != null){
            Destroy(this.gameObject);
        }else {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{

    public GameObject[] objects;

    void Awake()
    {
        foreach(var elem in objects){
            DontDestroyOnLoad(elem);
        }
    }
}

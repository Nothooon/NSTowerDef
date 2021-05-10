using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public float spawnTimer;
    private float timer;
    private Sprite[] spriteArray;

    // Arguments pour le script Follow The Path
    public Transform[] waypoints;
    public float moveSpeed;

    void Start(){
        timer = spawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0) {
            timer = spawnTimer;
            
        }else{
            timer -= Time.deltaTime;
        }
    }
}

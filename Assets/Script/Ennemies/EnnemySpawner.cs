using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spawnee;
    public Transform[] waypoints;
    public float timer = 1000; // Temps entre l'apparition de chaque ennemi (en secondes)

    // Update is called once per frame
    void Update()
    {
        if(timer == 0) {
            spawnee = Instantiate(spawnee, spawnPoint.position, spawnPoint.rotation);
            FollowThePath script = spawnee.GetComponent(typeof(FollowThePath)) as FollowThePath;
            spawnee.name = "Ennemy";
            script.SetWaypoints(waypoints);
            script.SetSpriteRenderer(spawnee.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer);
            timer = 1000;
        }
        timer --; 
    }
}

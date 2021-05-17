using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] spawnee;
    public GameObject instance;
    public Transform[] waypoints;

    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty


    void Start(){
        StartCoroutine(Waves());
    }


    IEnumerator Waves(){
        // WAVE 1
        points = 10;
        yield return new WaitForSecondsRealtime(5); // preparation time
        difficulty = 0.5f;
        while(points > 0){ 
            points -= SpawnEnnemies(points,difficulty);
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }

        // WAVE 2
        points = 20;
        yield return new WaitForSecondsRealtime(5); // preparation time
        difficulty = 0.2f;
        while(points > 0){ 
            points -= SpawnEnnemies(points,difficulty);
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }

        // WAVE 3
        points = 1000;
        yield return new WaitForSecondsRealtime(5); // preparation time
        difficulty = 0f;
        while(points > 0){ 
            points -= SpawnEnnemies(points,difficulty);
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }
        
    }
    int SpawnEnnemies(int points, float difficulty){
        float random = Random.Range(0f,1f);
        random += difficulty;
        if(random < 0.1){
            if(((points -= spawnee[3].GetComponent<Ennemy>().points) >=0)){
                instance = Instantiate(spawnee[3], spawnPoint.position, spawnPoint.rotation);
                FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
                //instance.name = "Ennemy";
                follow.SetWaypoints(waypoints);
                return spawnee[3].GetComponent<Ennemy>().points;
            }else{
                random += 0.1f;
            }
        }
        if(random < 0.2){
            if(((points -= spawnee[2].GetComponent<Ennemy>().points) >=0)){
                instance = Instantiate(spawnee[2], spawnPoint.position, spawnPoint.rotation);
                FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
                //instance.name = "Ennemy";
                follow.SetWaypoints(waypoints);
                return spawnee[2].GetComponent<Ennemy>().points;
            }else{
                random += 0.2f;
            }
        }
        if(random < 0.5){
            if(((points -= spawnee[1].GetComponent<Ennemy>().points) >=0)){
                instance = Instantiate(spawnee[1], spawnPoint.position, spawnPoint.rotation);
                FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
                //instance.name = "Ennemy";
                follow.SetWaypoints(waypoints);
                return spawnee[1].GetComponent<Ennemy>().points;
            }else{
                random += 0.5f;
            }
        }

        if(random > 0.5){
            if(((points -= spawnee[0].GetComponent<Ennemy>().points) >=0)){
                instance = Instantiate(spawnee[0], spawnPoint.position, spawnPoint.rotation);
                FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
                //instance.name = "Ennemy";
                follow.SetWaypoints(waypoints);
                return spawnee[0].GetComponent<Ennemy>().points;
            }
        }
        return 0;
    }
}


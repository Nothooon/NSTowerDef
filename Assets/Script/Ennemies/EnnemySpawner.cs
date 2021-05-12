using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] spawnee;
    public GameObject instance;
    public Transform[] waypoints;

    private int points;


    void Start(){
        StartCoroutine(Waves());
    }


    IEnumerator Waves(){
        // WAVE 1
        points = 10;
        yield return new WaitForSecondsRealtime(5); // preparation time
        while(points > 0){ 
            points -= SpawnEnnemies(points);
            yield return new WaitForSecondsRealtime(1);
        }

        // WAVE 2
        points = 10;
        yield return new WaitForSecondsRealtime(5); // preparation time
        while(points > 0){ 
            points -= SpawnEnnemies(points);
            yield return new WaitForSecondsRealtime(0.5f);
        }

        // WAVE 3
        points = 20;
        yield return new WaitForSecondsRealtime(5); // preparation time
        while(points > 0){ 
            points -= SpawnEnnemies(points);
            yield return new WaitForSecondsRealtime(0.25f);
        }
        
    }
    int SpawnEnnemies(int points){
        float random = Random.Range(0f,1f);
        if(random > 0){
            instance = Instantiate(spawnee[0], spawnPoint.position, spawnPoint.rotation);
            FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
            instance.name = "Ennemy";
            follow.SetWaypoints(waypoints);
            return instance.GetComponent<Ennemy>().points;
        }else{
            return 0;
        }        
    }


    public int GetPoints(){
        return this.points;
    }

}

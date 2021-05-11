using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject spawnee;
    public Transform[] waypoints;

    private int points;

        

    public float timer = 1000;

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
            spawnee = Instantiate(spawnee, spawnPoint.position, spawnPoint.rotation);
            FollowThePath script = spawnee.GetComponent(typeof(FollowThePath)) as FollowThePath;
            spawnee.name = "Ennemy";
            script.SetWaypoints(waypoints);
            script.SetSpriteRenderer(spawnee.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer);
            return 1;
        }else{
            return 0;
        }        
    }


    public int GetPoints(){
        return this.points;
    }

    // Update is called once per frame
    void Update()
    {/*
        if(timer == 0) {
            spawnee = Instantiate(spawnee, spawnPoint.position, spawnPoint.rotation);
            FollowThePath script = spawnee.GetComponent(typeof(FollowThePath)) as FollowThePath;
            spawnee.name = "Ennemy";
            script.SetWaypoints(waypoints);
            script.SetSpriteRenderer(spawnee.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer);
            timer = 1000;
        }
        timer--;*/
    }
}

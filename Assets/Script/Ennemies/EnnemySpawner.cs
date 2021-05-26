using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] spawnee;
    private GameObject instance;
    public Transform[] waypoints;
    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty
    private bool paused;
    private bool wavesFinished = false;


    void Start(){
        StartCoroutine(Waves());
        wavesFinished = false;
        paused = false;    
    }


    IEnumerator Waves(){
        // WAVE 1
        points = 10;
        yield return new WaitForSecondsRealtime(5); // preparation time
        WaveCounter.WaveActual++;
        difficulty = 0.5f;
        while(points > 0){ 
            if(!paused){
                points -= SpawnEnemies(points,difficulty);
                Debug.Log(points);

            }else{
                yield return new WaitUntil(() => !paused );
            }
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }
        //GetComponentInParent<GameOverManager>().onDefeat();

        // WAVE 2
        points = 20;
        yield return new WaitForSecondsRealtime(5); // preparation time
        WaveCounter.WaveActual++;
        difficulty = 0.2f;
        while(points > 0){ 
            if(!paused){
                points -= SpawnEnemies(points,difficulty);
                Debug.Log(points);
            }else{
                yield return new WaitUntil(() => !paused );
            }
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }

        // WAVE 3
        points = 50;
        yield return new WaitForSecondsRealtime(5); // preparation time
        WaveCounter.WaveActual++;
        difficulty = 0f;
        while(points > 0){ 
            if(!paused){
                points -= SpawnEnemies(points,difficulty);
                Debug.Log(points);
            }else{
                yield return new WaitUntil(() => !paused );
            }
            yield return new WaitForSecondsRealtime(1); // interval between enemies spawn
        }
        wavesFinished =  true;
        
    }
    public int SpawnEnemies(int points, float difficulty){
        float random = Random.Range(0f,1f);
        int enemyIndex = 0;
        random += difficulty;
        if(random < 0.1){
            if(((points -= spawnee[enemyIndex].GetComponent<Ennemy>().points) >=0)){
                return SpawnEnemy(enemyIndex);                
            }else{
                random += 0.1f;
            }
        }
        enemyIndex ++;
        if(random < 0.2){
            if(((points -= spawnee[enemyIndex].GetComponent<Ennemy>().points) >=0)){
                return SpawnEnemy(enemyIndex);  
            }else{
                random += 0.2f;
            }
        }
        enemyIndex ++;
        if(random < 0.5){
            if(((points -= spawnee[enemyIndex].GetComponent<Ennemy>().points) >=0)){
                return SpawnEnemy(enemyIndex);  
            }else{
                random += 0.5f;
            }
        }
        enemyIndex ++;
        if(random > 0.5){
            if(((points -= spawnee[enemyIndex].GetComponent<Ennemy>().points) >=0)){
                return SpawnEnemy(enemyIndex);  
            }
        }

        return 0;
    }



    public int SpawnEnemy(int enemyIndex){
        instance = Instantiate(spawnee[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
        //instance.name = "Ennemy";
        follow.SetWaypoints(waypoints);
        return spawnee[enemyIndex].GetComponent<Ennemy>().points;
    }

    public void pauseSpawn(){
        StopCoroutine("Waves");
        this.paused = true;
    }

    public void unPauseSpawn(){
        StartCoroutine("Waves");
        this.paused = false;
    }

    public void Update(){
        if(wavesFinished && points == 0 && FindObjectOfType<Ennemy>() == null){
            GetComponent<GameOverManager>().onSuccess();
        }
    }
}


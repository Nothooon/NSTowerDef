using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] spawnee; // an array of the enemies that will spawn in this level
    private GameObject instance;
    public Button nextWaveButton; // the button triggering the next wave
    public Transform[] waypoints;
    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty
    public bool triggerNextWave; // will trigger the next wave if true
    private bool wavesFinished = false;


    void Start(){
        wavesFinished = false;
        triggerNextWave = false;
        StartCoroutine(Waves());
    }


    IEnumerator Waves(){
        // WAVE 1
        yield return new WaitUntil(() => triggerNextWave);
        triggerNextWave = false;
        this.nextWaveButton.interactable = false;
        this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "next wave";
        WaveCounter.WaveActual++;
        yield return new WaitForSeconds(1); // smooth transition
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }



        // WAVE 2
        nextWaveButton.interactable = true;
        float preparationTime = Time.time + 5;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        yield return new WaitForSeconds(1); // smooth transition
        


        SpawnEnemy(2);
        yield return new WaitForSeconds(1); // interval between enemies spawn
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }



        // WAVE 3
        nextWaveButton.interactable = true;
        preparationTime = Time.time + 5;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        yield return new WaitForSeconds(1); // smooth transition
        

        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }

        wavesFinished =  true;
    }

    public void nextWave(){
        this.triggerNextWave = true;
    }

    private bool testNextWave(float timeInterval){
       if(triggerNextWave || Time.time >= timeInterval){
            this.nextWaveButton.interactable = false;
            triggerNextWave = false;
            return true;    
       }else{
           return false;
       }
    }
    
    /*
    public int SpawnRandomEnemies(int points, float difficulty){
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
*/


    public int SpawnEnemy(int enemyIndex){
        instance = Instantiate(spawnee[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
        //instance.name = "Ennemy";
        follow.SetWaypoints(waypoints);
        return spawnee[enemyIndex].GetComponent<Ennemy>().points;
    }
    public void Update(){
        if(wavesFinished && points == 0 && FindObjectOfType<Ennemy>() == null){
            GetComponent<GameOverManager>().onSuccess();
        }
    }
}


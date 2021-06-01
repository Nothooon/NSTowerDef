using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnnemySpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject[] spawnee;
    private GameObject instance;
    public Button nextWaveButton;
    public Transform[] waypoints;
    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty
    public bool paused;
    private bool wavesFinished = false;


    void Start(){
        
        wavesFinished = false;
        paused = false;    
    }

    public void launchWaves(){
        StartCoroutine(Waves());
        nextWaveButton.interactable = false;
    }

    IEnumerator Waves(){
        // WAVE 1
        yield return new WaitForSeconds(2); // preparation time
        WaveCounter.WaveActual++;
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }

        // WAVE 2
        yield return new WaitForSeconds(5); // preparation time
        WaveCounter.WaveActual++;
        SpawnEnemy(2);
        yield return new WaitForSeconds(1); // interval between enemies spawn
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }

        // WAVE 3
        yield return new WaitForSeconds(5); // preparation time
        WaveCounter.WaveActual++;
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }

        wavesFinished =  true;
    }
    
    /*
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


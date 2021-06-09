using System;
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

    private GameObject audioMixer;
    public AudioClip waveStartSound;

    private GameObject timerUntilNextWave;
    public Button nextWaveButton; // the button triggering the next wave
    public Transform[] waypoints;
    public GameObject arrow; // arrow that indicate the path of the enemies
    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty
    private bool triggerNextWave; // will trigger the next wave if true
    private bool wavesFinished = false;

    float preparationTime;
    int timerText;

    void Start(){
        wavesFinished = false;
        triggerNextWave = false;
        timerUntilNextWave = GameObject.Find("TimerNextWave");
        audioMixer = GameObject.Find("AudioManager");
        StartCoroutine(Waves());
    }

    IEnumerator SpawnEnemies(float delay, int enemyIndex, int amount){
        for (int i = 0; i < amount; i++)
        {
            SpawnEnemy(enemyIndex);
            yield return new WaitForSeconds(delay);
        }
    }


    IEnumerator createWave(bool isFirst)
    {

        if(isFirst){
            this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "begin";
        }else{
            this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "next wave";    
        }
        
        nextWaveButton.interactable = true;
        preparationTime = 90f;
        timerText = (int)Math.Round(preparationTime, 0);
        timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = "" + timerText;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        if(isFirst)
            arrow.SetActive(false);
        WaveCounter.WaveActual++;

        yield return new WaitForSeconds(1); // smooth transition

    }


    IEnumerator Waves(){


        triggerNextWave = false;


        // WAVE 1
        yield return StartCoroutine(createWave(true));

        yield return StartCoroutine(SpawnEnemies(0.5f,3,5));
        yield return StartCoroutine(SpawnEnemies(0.5f,2,3));
        yield return StartCoroutine(SpawnEnemies(0.5f,1,1));
        yield return StartCoroutine(SpawnEnemies(0.5f,3,10));


        // WAVE 2
        yield return StartCoroutine(createWave(false));

        yield return StartCoroutine(SpawnEnemies(0.5f,3,5));
        yield return StartCoroutine(SpawnEnemies(0.5f,2,3));
        yield return StartCoroutine(SpawnEnemies(0.5f,1,2));
        yield return StartCoroutine(SpawnEnemies(0.5f,3,10));
        yield return StartCoroutine(SpawnEnemies(1f,0,2));
        yield return StartCoroutine(SpawnEnemies(1f,3,10));

        //WAVE 3
        yield return StartCoroutine(createWave(false));

        yield return StartCoroutine(SpawnEnemies(0.5f,3,10));
        yield return StartCoroutine(SpawnEnemies(0.5f,2,5));
        yield return StartCoroutine(SpawnEnemies(0.5f,1,2));
        yield return StartCoroutine(SpawnEnemies(0.5f,3,10));
        yield return StartCoroutine(SpawnEnemies(1f,0,2));
        yield return StartCoroutine(SpawnEnemies(1f,3,10));

        wavesFinished =  true;
    }

    public void nextWave(){
        this.triggerNextWave = true;
        nextWaveButton.interactable = false;
        timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = "" + 0;
    }

    private bool testNextWave(float timeInterval){
       if(triggerNextWave || 0 >= timeInterval){
            this.nextWaveButton.interactable = false;
            triggerNextWave = false;
            audioMixer.GetComponent<audioManager>().playSound(waveStartSound);
            return true;    
       }else{
           preparationTime-= Time.deltaTime;
           timerText = (int) Math.Round(preparationTime, 0);
           timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = ""+timerText;
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


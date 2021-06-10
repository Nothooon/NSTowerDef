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
    public AudioClip WaveStartSound;

    private GameObject timerUntilNextWave;
    public Button nextWaveButton; // the button triggering the next wave
    public Transform[] waypoints;
    public GameObject arrow; // arrow that indicate the path of the enemies
    private int points; // the higher the number, the more enemies will spawn
    private float difficulty; // the higher the number, the lower is the difficulty
    private bool triggerNextWave; // will trigger the next wave if true
    private bool WavesFinished = false;

    float preparationTime;
    int timerText;

    public int level; // Which level is playing
    /* Ennemies Indexes*/

    int DRAGON = 0;
    int KNIGHT = 1;
    int BAT = 2;
    int SLIME = 3;
    int GHOST = 4;


    void Start(){
        WavesFinished = false;
        triggerNextWave = false;
        timerUntilNextWave = GameObject.Find("TimerNextWave");
        audioMixer = GameObject.Find("AudioManager");
        switch(level){
            case 1 :
                StartCoroutine(FirstLevelWaves());
                break;
            case 2 :
                StartCoroutine(SecondLevelWaves());
                break;
            default:
                Debug.Log("Level number doesn't exists");
                break;
        }
        
    }

    // Used to spawn 'amount' ennemies of the same 'enemyIndex' type with 'delay' seconds between each spawn.
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

    // Function called for first level waves
    IEnumerator FirstLevelWaves(){
        WaveCounter.WaveTotal = 9; // Indicates the total number of waves to 
        triggerNextWave = false;

        // WAVE 1
        yield return StartCoroutine(createWave(true));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 5));
        
        // WAVE 2
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 10));
        
        //WAVE 3
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.2f, SLIME, 15));

        //WAVE 4
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 5));

        //WAVE 5
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.2f, BAT, 10));

        //WAVE 6
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 10));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 5));

        //WAVE 7
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, KNIGHT, 1));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 10));
        yield return StartCoroutine(SpawnEnemies(0.5f, KNIGHT, 1));

        //WAVE 8
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, KNIGHT, 5));

        //WAVE 9
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(3f, DRAGON, 3));

        //WAVE 9
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.1f, SLIME, 50));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 20));
        yield return StartCoroutine(SpawnEnemies(0.5f, KNIGHT, 10));
        yield return StartCoroutine(SpawnEnemies(0.5f, DRAGON, 10));

        WavesFinished =  true;
    }

    // Function called for second level waves
    IEnumerator SecondLevelWaves(){
        WaveCounter.WaveTotal = 9;
        triggerNextWave = false;

        // WAVE 1
        yield return StartCoroutine(createWave(true));
        yield return StartCoroutine(SpawnEnemies(0.8f, SLIME, 3));
        yield return new WaitForSeconds(2.2f);
        SpawnEnemy(GHOST);
        yield return new WaitForSeconds(2.2f);
        yield return StartCoroutine(SpawnEnemies(0.8f, SLIME, 3));

        // WAVE 2
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(1.2f, SLIME, 5));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(SpawnEnemies(1.5f, GHOST, 3));
        yield return new WaitForSeconds(3.5f);
        SpawnEnemy(KNIGHT);
        
        //WAVE 3
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(1f, SLIME, 15));
        yield return StartCoroutine(SpawnEnemies(1f, GHOST, 3));

        //WAVE 4
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.8f, BAT, 7));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 5));

        //WAVE 5
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 5));
        yield return StartCoroutine(SpawnEnemies(0.5f, GHOST, 3));
        yield return new WaitForSeconds(3.5f);
        yield return StartCoroutine(SpawnEnemies(2.5f, KNIGHT, 3));

        //WAVE 6
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 10));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 5));
        yield return StartCoroutine(SpawnEnemies(0.5f, GHOST, 5));
        yield return StartCoroutine(SpawnEnemies(3f, KNIGHT, 2));

        //WAVE 7
        yield return StartCoroutine(createWave(false));
        SpawnEnemy(KNIGHT);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(SpawnEnemies(0.5f, SLIME, 20));
        SpawnEnemy(KNIGHT);

        //WAVE 8
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(2f, KNIGHT, 2));
        yield return StartCoroutine(SpawnEnemies(0.5f, GHOST, 5));
        yield return StartCoroutine(SpawnEnemies(2f, KNIGHT, 2));
        yield return StartCoroutine(SpawnEnemies(1f, SLIME, 25));

        //WAVE 9
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(5f, DRAGON, 2));
        yield return StartCoroutine(SpawnEnemies(1f, GHOST, 5));
        yield return StartCoroutine(SpawnEnemies(5f, DRAGON, 2));

        //WAVE 9
        yield return StartCoroutine(createWave(false));
        yield return StartCoroutine(SpawnEnemies(0.9f, GHOST, 10));
        yield return StartCoroutine(SpawnEnemies(0.6f, SLIME, 5));
        yield return StartCoroutine(SpawnEnemies(0.5f, BAT, 20));
        yield return StartCoroutine(SpawnEnemies(0.6f, SLIME, 5));
        yield return StartCoroutine(SpawnEnemies(2.5f, KNIGHT, 3));
        yield return StartCoroutine(SpawnEnemies(0.6f, SLIME, 5));
        SpawnEnemy(DRAGON);
        yield return new WaitForSeconds(3f);
        yield return StartCoroutine(SpawnEnemies(0.6f, SLIME, 5));

        WavesFinished =  true;
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
            audioMixer.GetComponent<audioManager>().playSound(WaveStartSound);
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

    // Spawns one enemy of the given index. 
    public int SpawnEnemy(int enemyIndex){
        instance = Instantiate(spawnee[enemyIndex], spawnPoint.position, spawnPoint.rotation);
        FollowThePath follow = instance.GetComponent(typeof(FollowThePath)) as FollowThePath;
        //instance.name = "Ennemy";
        follow.SetWaypoints(waypoints);
        return spawnee[enemyIndex].GetComponent<Ennemy>().points;
    }
    public void Update(){
        if(WavesFinished && points == 0 && FindObjectOfType<Ennemy>() == null){
            GetComponent<GameOverManager>().onSuccess();
        }
    }
}


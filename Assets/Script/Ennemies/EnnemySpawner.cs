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

    public class waveContent
    {
        public float interval;
        public int nbMonsters;
        public int monsterType;
            
            public waveContent(float time, int nb, int mt)
        {
            this.interval = time;
            this.nbMonsters = nb;
            this.monsterType = mt;
        }
    }

    IEnumerator launchContent (waveContent wC)
    {
        for (int i = 0; i < wC.nbMonsters; i++)
        {
            SpawnEnemy(wC.monsterType);
            yield return new WaitForSeconds(wC.interval); // interval between enemies spawn
        }
    }

    IEnumerator createWave(List<waveContent> wCL)
    {
        this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "begin";
        preparationTime = 90f;
        timerText = (int)Math.Round(preparationTime, 0);
        timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = "" + timerText;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        arrow.SetActive(false);

        yield return new WaitForSeconds(1); // smooth transition

        foreach(waveContent wC in wCL)
        {
            yield return StartCoroutine(launchContent(wC));
        }

    }


    IEnumerator Waves(){


        triggerNextWave = false;

        List<waveContent> wave = new List<waveContent>();


        // WAVE 1
        wave.Add(new waveContent(0.5f, 5, 3));
        wave.Add(new waveContent(0.5f, 3, 2));
        wave.Add(new waveContent(0.5f, 1, 1));
        wave.Add(new waveContent(0.5f, 10, 3));

        yield return StartCoroutine(createWave(wave));
        wave.Clear();

        //WAVE 2
        wave.Add(new waveContent(0.5f, 5, 3));
        wave.Add(new waveContent(0.5f, 3, 2));
        wave.Add(new waveContent(1f  , 2, 1));
        wave.Add(new waveContent(0.5f, 10, 3));
        wave.Add(new waveContent(1f  , 2, 0));
        wave.Add(new waveContent(0.5f, 10, 3));

        yield return StartCoroutine(createWave(wave));
        wave.Clear();

        //WAVE 3
        wave.Add(new waveContent(0.5f, 10, 3));
        wave.Add(new waveContent(0.5f, 5, 2));
        wave.Add(new waveContent(1f, 2, 1));
        wave.Add(new waveContent(0.5f, 10, 3));
        wave.Add(new waveContent(1f, 2, 0));
        wave.Add(new waveContent(0.5f, 10, 3));

        yield return StartCoroutine(createWave(wave));
        wave.Clear();

        /*
        // WAVE 1
        this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "begin";
        preparationTime = 90f;
        timerText = (int) Math.Round(preparationTime, 0);
        timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = "" + timerText;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        arrow.SetActive(false);

        yield return new WaitForSeconds(1); // smooth transition
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(2);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 1; i++)
        {
            SpawnEnemy(1);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }



        // WAVE 2
        this.nextWaveButton.transform.GetChild(0).GetComponent<Text>().text = "next wave";
        nextWaveButton.interactable = true;
        preparationTime = 90f;
        timerText = (int) Math.Round(preparationTime, 0);
        timerUntilNextWave.GetComponent<TextMeshProUGUI>().text = "" + timerText;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        yield return new WaitForSeconds(1); // smooth transition
        


        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 3; i++)
        {
            SpawnEnemy(2);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(1);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(0);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }



        // WAVE 3
        nextWaveButton.interactable = true;
        preparationTime = 90f;
        yield return new WaitUntil(() => testNextWave(preparationTime));
        WaveCounter.WaveActual++;
        yield return new WaitForSeconds(1); // smooth transition
        

        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 5; i++)
        {
            SpawnEnemy(2);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(1);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        for (int i = 0; i < 2; i++)
        {
            SpawnEnemy(0);
            yield return new WaitForSeconds(1); // interval between enemies spawn
        }
        for (int i = 0; i < 10; i++)
        {
            SpawnEnemy(3);
            yield return new WaitForSeconds(0.5f); // interval between enemies spawn
        }
        */
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


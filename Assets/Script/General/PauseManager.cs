using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPaused;
    public GameObject pauseUI;
    public GameObject moneyCounter;
    public GameObject lifeCounter;
    public GameObject waveCounter;
    public GameObject shop;
    public GameObject turrentBuilder;
    public GameObject[] powerButons;
    public static PauseManager instance;
    //private bool cantPause;

    private  void Awake(){
        if(instance != null){
            Debug.LogWarning("more than 1 instance of GameManager");
            return;
        }
        instance = this;
        isPaused = false;
        Time.timeScale = 1;
    }

     public void pauseGame(bool display){ // true display the Pause UI
        if(display) 
            pauseUI.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;

        turrentBuilder.SetActive(false);
        shop.SetActive(false);
        waveCounter.SetActive(false);
        lifeCounter.SetActive(false);
        moneyCounter.SetActive(false);
        foreach(GameObject button in powerButons){
            button.SetActive(false);
        }
        gameObject.GetComponent<EnnemySpawner>().pauseSpawn();
    }

    public void unPauseGame(){
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        
        turrentBuilder.SetActive(true);
        shop.SetActive(true);
        waveCounter.SetActive(true);
        lifeCounter.SetActive(true);
        moneyCounter.SetActive(true);
        foreach(GameObject button in powerButons){
            button.SetActive(true);
        }
        
        gameObject.GetComponent<EnnemySpawner>().unPauseSpawn();
    }

    // Update is called once per frame
    void Update(){ 
        if(Input.GetKeyDown("escape")){
            if(isPaused){
                unPauseGame();
            }else{
                pauseGame(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPaused;
    public GameObject pauseUI;
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
        transform.GetChild(0).gameObject.GetComponent<EnnemySpawner>().pauseSpawn();
    }

    public void unPauseGame(){
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        Debug.Log("unposed game");
        transform.GetChild(0).gameObject.GetComponent<EnnemySpawner>().unPauseSpawn();
    }
    /*
    public void setCantPause(bool val){
        cantPause = val;
    }
    */
    // Update is called once per frame
    void Update(){
        //if(!cantPause){
            if(Input.GetKeyDown("escape")){
                if(isPaused){
                    unPauseGame();
                }else{
                    pauseGame(true);
                }
            }
        //}
    }
}

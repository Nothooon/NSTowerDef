using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public static GameOverManager instance;

    private  void Awake(){
        if(instance != null){
            Debug.LogWarning("more than 1 instance of GameManager");
            return;
        }
        instance = this;
    }

    public void retryButton(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOverUI.SetActive(false);
    }

    public void mainMenuButton(){
        
    }

    public void onDefeat(){
        gameOverUI.SetActive(true);
        //Time.timeScale = 0;
    }

    public void setUI(GameObject ui){
        this.gameOverUI = ui;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    public GameObject defeatUI;
    public GameObject successUI;

    public static GameOverManager instance;

    private  void Awake(){
        if(instance != null){
            Debug.LogWarning("more than 1 instance of GameManager");
            return;
        }
        instance = this;
    }

    public void retryButton(){
        GetComponent<PauseManager>().unPauseGame();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void mainMenuButton(){
        GetComponent<PauseManager>().unPauseGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void onDefeat(){
        defeatUI.SetActive(true);
        GetComponent<PauseManager>().pauseGame(false);
    }

    public void onSuccess(){
        successUI.SetActive(true);
    }

    public void setGameOverUI(GameObject ui){
        this.defeatUI = ui;
    }

    public void setSuccessUI(GameObject ui){
        this.successUI = ui;
    }  

    public void changeScene(string scene){
        SceneManager.LoadScene(scene);
    }
}

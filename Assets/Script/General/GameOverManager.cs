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
        string currentSceneName = SceneManager.GetActiveScene().name;
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneName);
    }

    public void mainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void onDefeat(){
        defeatUI.SetActive(true);
        Time.timeScale = 0;
        transform.GetChild(0).gameObject.SetActive(false);
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
}

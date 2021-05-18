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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //defeatUI.SetActive(false);
        //successUI.SetActive(false);
    }

    public void mainMenuButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void onDefeat(){
        defeatUI.SetActive(true);
    }

    public void onSuccess(){
        successUI.SetActive(true);
    }

    public void setGameOverUI(GameObject ui){
        this.defeatUI = ui;
        StopAllCoroutines();
    }

    public void setSuccessUI(GameObject ui){
        this.successUI = ui;
    }
}

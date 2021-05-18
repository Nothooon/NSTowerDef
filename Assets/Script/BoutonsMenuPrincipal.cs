using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonsMenuPrincipal : MonoBehaviour
{
    
    private GameObject gameOverUI;
    private GameObject successUI;
    public GameObject gameManager;

    public void launchFirstLevel(){
        SceneManager.LoadScene("SampleScene");
        gameOverUI = GameObject.Find("GameOverMenu");
        successUI = GameObject.Find("SuccessMenu");
        gameManager.GetComponent<GameOverManager>().setGameOverUI(gameOverUI);
        gameManager.GetComponent<GameOverManager>().setSuccessUI(successUI);
    }

    public void QuitGame(){
        Debug.Log("User quitted the game");
        Application.Quit();
    }
}

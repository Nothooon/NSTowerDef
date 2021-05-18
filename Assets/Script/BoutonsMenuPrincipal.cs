using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonsMenuPrincipal : MonoBehaviour
{
    
    private GameObject gameOverUI;
    public GameObject gameManager;

    public void LancerJeu(){
        SceneManager.LoadScene("SampleScene");
        gameOverUI = GameObject.Find("GameOverMenu");
        gameManager.GetComponent<GameOverManager>().setUI(gameOverUI);
    }

    public void QuitterJeu(){
        Debug.Log("User quitted the game");
        Application.Quit();
    }
}

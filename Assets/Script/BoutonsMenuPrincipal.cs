using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoutonsMenuPrincipal : MonoBehaviour
{


    public void launchFirstLevel(){
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame(){
        Debug.Log("User quitted the game");
        Application.Quit();
    }
}

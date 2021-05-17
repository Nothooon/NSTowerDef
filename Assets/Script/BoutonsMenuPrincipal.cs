using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonsMenuPrincipal : MonoBehaviour
{
    public void LancerJeu(){
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitterJeu(){
        Debug.Log("User quitted the game");
        Application.Quit();
    }
}

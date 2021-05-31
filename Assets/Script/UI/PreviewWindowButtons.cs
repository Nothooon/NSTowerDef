using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PreviewWindowButtons : MonoBehaviour
{
    public void backButton(){
        SceneManager.LoadScene("MainMenu");
    }

    public void beginButton(){
        SceneManager.LoadScene("SampleScene");
    }
}

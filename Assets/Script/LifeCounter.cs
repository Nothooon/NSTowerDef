using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCounter : MonoBehaviour
{

    public static int LifeValue = 100;
    Text life;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        bool defeat = false;
        life.text = "" + LifeValue;
        if (LifeValue <= 0 && !defeat)
        {
            //GetComponentInParent<GameOverManager>().onDefeat();
            GameOverManager.instance.onDefeat();
            defeat = true;
        }
    }
}
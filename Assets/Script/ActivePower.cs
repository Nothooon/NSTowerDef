using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePower : MonoBehaviour
{
    public GameObject boomButton;
    private bool isBoomOnCooldown = false;
    private float boomCooldown = 5;
    private float nextBoomTime = 0;

    void Start(){
        boomCooldown = 2;
        nextBoomTime = 0;
        isBoomOnCooldown = false;
        boomButton.GetComponent<Image>().fillAmount = 1;
    }
    public void boom(){
        if(!isBoomOnCooldown){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies){
                enemy.GetComponent<Ennemy>().takeDamage(10);
            }
            boomButton.GetComponent<Image>().fillAmount = 0;
            nextBoomTime = Time.time + boomCooldown;
            isBoomOnCooldown = true;
        }
    }

    void Update(){
        if(Time.time >= nextBoomTime){
            isBoomOnCooldown = false;
        }else{
            boomButton.GetComponent<Image>().fillAmount += 1f / boomCooldown * Time.deltaTime;
        }
    }
}

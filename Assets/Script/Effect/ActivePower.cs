using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePower : MonoBehaviour
{
    public GameObject boomButton;
    private bool isBoomOnCooldown = false;
    private float boomCooldown = 25;
    private float nextBoomTime = 0;
    public GameObject boomAnimation;


    public GameObject quickButton;
    public List<GameObject> turrets;
    private bool isQuickShotOnCooldown = false;
    private float quickShotCooldown = 30;
    private float nextQuickShotTime = 0;
    private float quickShotDuration = 5;
    private float endQuickShotTime = 0;

    void Start(){
        
    }
    public void boom(){
        if(!isBoomOnCooldown){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(GameObject enemy in enemies){
                enemy.GetComponent<Ennemy>().takeDamage(10);
                Instantiate(boomAnimation, enemy.transform.position, new Quaternion(0,0,0,0));
            }
            GameObject[] enemies2 = GameObject.FindGameObjectsWithTag("EnemyGhost");
            foreach (GameObject enemy in enemies2)
            {
                enemy.GetComponent<Ennemy>().takeDamage(10);
                Instantiate(boomAnimation, enemy.transform.position, new Quaternion(0, 0, 0, 0));
            }
            boomButton.GetComponent<Image>().fillAmount = 0;
            nextBoomTime = Time.time + boomCooldown;
            isBoomOnCooldown = true;
        }
    }

    public void quickShot(){
        if(!isQuickShotOnCooldown){
            turrets.AddRange(GameObject.FindGameObjectsWithTag("Turret")); 
            foreach(GameObject turret in turrets.ToArray()){
                turret.GetComponentInChildren<turretSelection>().fireRate *= 1.5f;
            }
            quickButton.GetComponent<Image>().fillAmount = 0;
            nextQuickShotTime = Time.time + quickShotCooldown;
            endQuickShotTime = Time.time + quickShotDuration;
            isQuickShotOnCooldown = true;
        }
    }

    public void addTurret(GameObject turret){
        turret.GetComponent<turretSelection>().fireRate *= 1.5f;
        turrets.Add(turret);
    }

    void Update(){
        if(Time.time >= nextBoomTime){
            isBoomOnCooldown = false;
        }else{
            boomButton.GetComponent<Image>().fillAmount += 1f / boomCooldown * Time.deltaTime;
        }

        if(Time.time >= nextQuickShotTime){
            isQuickShotOnCooldown = false;
            
        }else{
            quickButton.GetComponent<Image>().fillAmount += 1f / quickShotCooldown * Time.deltaTime;
        }

        if(isQuickShotOnCooldown){
            if(Time.time >= endQuickShotTime){
                foreach(GameObject turret in turrets){
                    turret.GetComponentInChildren<turretSelection>().fireRate = turret.GetComponentInChildren<turretSelection>().fireRate / 3 *2;
                    Debug.Log("test");
                }
                turrets.Clear();
            }
        }
    }
}

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
    public GameObject boomAnimation;


    public GameObject quickButton;
    public List<GameObject> turrets;
    private bool isQuickShotOnCooldown = false;
    private float quickShotCooldown = 5;
    private float nextQuickShotTime = 0;
    private float quickShotDuration = 2;
    private float endQuickShotTime = 0;

    void Start(){
        boomCooldown = 2;
        nextBoomTime = 0;
        isBoomOnCooldown = false;
        boomButton.GetComponent<Image>().fillAmount = 1;
        quickShotCooldown = 5;
        nextQuickShotTime = 0;
        endQuickShotTime = 0;
        isQuickShotOnCooldown = false;
        quickButton.GetComponent<Image>().fillAmount = 1;
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
                turret.GetComponent<turretSelection>().fireRate *= 1.5f;
                turrets.Add(turret);
                
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
                    Debug.Log("test");
                    turret.GetComponent<turretSelection>().fireRate = turret.GetComponent<turretSelection>().fireRate / 3 *2;
                }
                turrets.Clear();
            }
        }
    }
}

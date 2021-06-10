using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ennemy : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public int damage;
    public int reward;
    public int points;
    public float speed;
    [SerializeField] float currentSpeed;

    public Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        
        currentHealth = maxHealth;
        slider.maxValue = maxHealth; 
        slider.value = currentHealth;
        currentSpeed = speed;
    }


    public void takeDamage(int damage){
        this.currentHealth -= damage;
        slider.value = currentHealth;
        if(currentHealth <= 0){
            MoneyCounter.MoneyValue += reward;
            Destroy(gameObject);
        }
    }    

    public float GetSpeed()
    {
        return speed;
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

    /**
     * Change the gameObject speed to the speed given for length seconds
     * If no length is given, the speed is forever changed 
     */ 
    public void SetSpeed(float speed, float length = 0f )
    {
        if (length == 0f)
        {
            this.currentSpeed = speed;
        } 
        else
        {
            StartCoroutine(ChangeSpeed(speed, length));
        }
    }

    IEnumerator ChangeSpeed(float speed, float length = 0f)
    {
        this.currentSpeed = speed;
        yield return new WaitForSeconds(length);
        this.currentSpeed = this.speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("hit");
        if(this.gameObject.tag == "EnemyGhost"){
            this.gameObject.tag = "Enemy";
        }

        if (collision.GetComponent<projectileBallista>() != null)
        {
            takeDamage(collision.GetComponent<projectileBallista>().power);
            collision.GetComponent<projectileBallista>().power--;
            if(collision.GetComponent<projectileBallista>().power <= 0)
            {
                Destroy(collision.GetComponent<projectileBallista>().gameObject);
            }
        }
    }

}

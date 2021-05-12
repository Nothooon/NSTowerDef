using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    public int damage;
    public int reward;
    public int points;
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void takeDamage(int damage){
        this.currentHealth -= damage;
        if(currentHealth <= 0){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

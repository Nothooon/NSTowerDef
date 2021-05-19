using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileCanon : MonoBehaviour
{

    public int power = 12;
    public GameObject target;
    float targetSize = 0.5f;
    public float range = 2f;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetVector = gameObject.transform.position - target.transform.position;
            if (targetVector.magnitude <= targetSize) // if the projectile hit the target
            {
                TouchTarget();
                Destroy(gameObject); // Destroy the projectile after it touches the target
            }
        }
    }

    // What we do when the projectile hit the target (depends of the type of projectile)
    void TouchTarget()
    {
        // On récupère tout les ennemies à porter
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, range, Vector2.zero);
        foreach (RaycastHit2D enemy in hit) 
        {
            if(enemy.collider.gameObject.tag == "Enemy")
            {
                Vector3 enemyVector = gameObject.transform.position - enemy.collider.transform.position;
                float degat = power * (range - enemyVector.magnitude) / range;
                enemy.collider.GetComponent<Ennemy>().takeDamage((int) degat);
            }
            
        } 
    }

    // Destroy the projectile if is out of bound
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

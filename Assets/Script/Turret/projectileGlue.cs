using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileGlue : MonoBehaviour
{
    [SerializeField] GameObject target;
    float targetSize = 0.5f;

    [SerializeField]
    float duration;

    private void Awake()
    {
        duration = 5f;
    }

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
        Ennemy enemy = target.GetComponent<Ennemy>();
        enemy.SetSpeed(enemy.GetSpeed()/2, duration);        
    }

    // Destroy the projectile if is out of bound
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    public void Upgrade()
    {
        duration = 7.5f; 
    }
}

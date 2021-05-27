using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBallista : MonoBehaviour
{
    public int power = 2;
    [SerializeField] GameObject target;
    float targetSize = 0.5f;
    public GameObject boomAnimation;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 targetVector = gameObject.transform.position - target.transform.position;
            if (targetVector.magnitude <= targetSize) // if the projectile hit the target
            {
                TouchTarget();
            }
        }
    }

    // What we do when the projectile hit the target (depends of the type of projectile)
    void TouchTarget()
    {
        target.GetComponent<Ennemy>().takeDamage(power);
        GameObject boom = Instantiate(boomAnimation, transform.position, new Quaternion(0, 0, 0, 0));
        boom.transform.localScale = boom.transform.localScale * 0.25f;
        //Destroy(gameObject); // Destroy the projectile after it touches the target
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
}
